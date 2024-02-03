using Newtonsoft.Json;
using PiControllerShared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private TcpClient connection = new();

        public event EventHandler? ClientConnected;
        public event EventHandler<(Guid, int)>? NoteReceived;

        private Queue<MessageData> messages = new();

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
                _ = Task.Run(() => handleClient(client));
            }
        }

        NetworkStream? stream = null;

        private async Task handleClient(TcpClient client)
        {
            this.connection = client;

            this.ClientConnected?.Invoke(this, new EventArgs());
            this.stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[1024 * 1024 * 52];
                while (true)
                {
                    while (this.messages.Count > 0)
                    {
                        MessageData dataOut = this.messages.Dequeue();
                        await this.sendRaw(dataOut);
                    }

                    if (stream.DataAvailable == false)
                    {
                        await Task.Delay(10);
                        continue;
                    }

                    stream.Read(buffer, 0, 4);
                    int frameSize = BitConverter.ToInt32(buffer, 0);

                    int readSize = 0;
                    while (readSize < frameSize)
                    {
                        while (!stream.DataAvailable)
                        {
                            await Task.Delay(10);
                        }
                        readSize += await stream.ReadAsync(buffer, readSize, frameSize - readSize);
                    }

                    string data = Encoding.UTF8.GetString(buffer, 0, readSize);

                    if (data.Length < 38)
                    {
                        Debug.WriteLine($"Invalid data received: {data}");
                        continue;
                    }

                    if (!Guid.TryParse(data[0..36], out Guid guid))
                    {
                        Debug.WriteLine($"Invalid data received: {data}");
                        continue;
                    }

                    if (!int.TryParse(data[37..].Trim(), out int value))
                    {
                        Debug.WriteLine($"Invalid data received: {data}");
                        continue;
                    }

                    this.NoteReceived?.Invoke(this, (guid, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                try
                {
                    this.connection.Close();
                }
                catch { }
                try
                {
                    this.connection.Dispose();
                }
                catch { }
            }
        }

        public void SendNote(Guid controlId, int value)
        {
            this.messages.Enqueue(new ValueMessageData(controlId, value));
        }

        public void SendColor(Guid controlId, int red, int green, int blue)
        {
            this.messages.Enqueue(new ColorMessageData(controlId, red, green, blue));
        }

        public void SendRaw(MessageData data)
        {
            this.messages.Enqueue(data);
        }

        private async Task sendRaw(MessageData data)
        {
            if (this.stream is null)
            {
                Console.WriteLine("Stream is null");
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            }));

            await this.stream.WriteAsync(BitConverter.GetBytes(buffer.Length).AsMemory(0, 4), new CancellationTokenSource(100).Token);
            await this.stream.WriteAsync(buffer, new CancellationTokenSource(100).Token);
            await this.stream.FlushAsync();
        }
    }
}
