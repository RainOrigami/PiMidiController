using Newtonsoft.Json;
using PiControllerShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PiControllerServer
{
    internal class Server
    {
        private Dictionary<Guid, TcpClient> connections = new();

        public event EventHandler<Guid>? ClientConnected;
        public event EventHandler<(Guid, int)>? NoteReceived;

        public Server(int port)
        {
            Task.Run(() => handle(port));
        }

        private async Task handle(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Task.Run(() => handleClient(client));
            }
        }


        private async Task handleClient(TcpClient client)
        {
            Guid clientGuid = Guid.NewGuid();
            this.connections.Add(clientGuid, client);

            this.ClientConnected?.Invoke(this, clientGuid);

            using (StreamReader stream = new StreamReader(client.GetStream()))
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        string? data = await stream.ReadLineAsync();

                        if (data == null)
                        {
                            throw new Exception("Lost connection to client");
                        }

                        if (data.Length < 38)
                        {
                            await Console.Out.WriteLineAsync($"Invalid data received: {data}");
                            continue;
                        }

                        if (!Guid.TryParse(data[0..36], out Guid guid))
                        {
                            await Console.Out.WriteLineAsync($"Invalid data received: {data}");
                            continue;
                        }

                        if (!int.TryParse(data[37..].Trim(), out int value))
                        {
                            await Console.Out.WriteLineAsync($"Invalid data received: {data}");
                            continue;
                        }

                        this.NoteReceived?.Invoke(this, (guid, value));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    this.connections.Remove(clientGuid);
                }
            }
        }

        public async Task SendNote(Guid controlId, int value)
        {
            await SendRaw(new ValueMessageData(controlId, value));
        }

        public async Task SendColor(Guid controlId, int red, int green, int blue)
        {
            await SendRaw(new ColorMessageData(controlId, red, green, blue));
        }

        public async Task SendRaw(MessageData data)
        {
            List<Task> sendingTasks = new List<Task>();
            foreach (KeyValuePair<Guid, TcpClient> connection in this.connections)
            {
                sendingTasks.Add(SendRaw(connection.Key, data));
            }
            await Task.WhenAll(sendingTasks);
        }

        public async Task SendRaw(Guid clientGuid, MessageData data)
        {
            if (this.connections.TryGetValue(clientGuid, out TcpClient? client) == false)
            {
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            }) + '\n');
            await client.GetStream().WriteAsync(buffer, 0, buffer.Length, new CancellationTokenSource(100).Token);
            await client.GetStream().FlushAsync();
        }
    }
}
