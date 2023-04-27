using Newtonsoft.Json;
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

            using (NetworkStream stream = client.GetStream())
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string data = Encoding.UTF8.GetString(buffer, 0, read);

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
            await SendRaw((controlId, value));
        }

        public async Task SendRaw(object data)
        {
            List<Task> sendingTasks = new List<Task>();
            foreach (KeyValuePair<Guid, TcpClient> connection in this.connections)
            {
                sendingTasks.Add(SendRaw(connection.Key, data));
            }
            await Task.WhenAll(sendingTasks);
        }

        public async Task SendRaw(Guid clientGuid, object data)
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
