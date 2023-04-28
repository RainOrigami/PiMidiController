using Gtk;
using Newtonsoft.Json;
using PiControllerShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerClient
{
    internal class Connection : IDisposable
    {
        private readonly string host;
        private readonly int port;
        private TcpClient? client;

        public event EventHandler<PageDefinition[]>? PageDefinitionsReceived;
        public event EventHandler<(Guid controlId, int value)>? ValueReceived;
        public event EventHandler<(Guid controlId, int red, int green, int blue)>? ColorReceived;

        public Connection(string host, int port)
        {
            this.host = host;
            this.port = port;

            Task.Run(handle);
        }

        public async Task SendButton(Guid controlId, bool on)
        {
            await SendControl(controlId, on ? 127 : 0);
        }

        public async Task SendKnob(Guid controlId, int value)
        {
            await SendControl(controlId, value);
        }

        public async Task SendControl(Guid controlId, int value)
        {
            await Send($"{controlId}-{value}");
        }

        public async Task Send(string raw)
        {
            while (this.client == null)
            {
                await connect();
            }

            try
            {
                StreamWriter streamWriter = new(this.client.GetStream());
                await streamWriter.WriteLineAsync(raw);
                await streamWriter.FlushAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }
        }

        private async Task handle()
        {
            Console.WriteLine("Connection thread started");
            while (this.client == null)
            {
                await connect();
            }

            StreamReader streamReader = new(this.client.GetStream());

            while (true)
            {
                try
                {
                    string? receivedData = null;
                    receivedData = await streamReader.ReadLineAsync();

                    if (receivedData == null)
                    {
                        await Task.Delay(1);
                        continue;
                    }

                    MessageData? messageData = JsonConvert.DeserializeObject<MessageData>(receivedData, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                    if (messageData == null)
                    {
                        await Console.Out.WriteLineAsync("Received invalid message data");
                        continue;
                    }

                    switch (messageData)
                    {
                        case PageDefinitionsMessageData pageDefinitionsMessageData:
                            this.PageDefinitionsReceived?.Invoke(this, pageDefinitionsMessageData.Definitions);
                            continue;
                        case ColorMessageData colorMessageData:
                            this.ColorReceived?.Invoke(this, (colorMessageData.ControlId, colorMessageData.Red, colorMessageData.Green, colorMessageData.Blue));
                            continue;
                        case ValueMessageData valueMessageData:
                            this.ValueReceived?.Invoke(this, (valueMessageData.ControlId, valueMessageData.Value));
                            continue;
                        default:
                            await Console.Out.WriteLineAsync("Unmapped message data received");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await connect();
                    streamReader = new(this.client.GetStream());
                }
            }
        }

        private async Task connect()
        {
            Console.WriteLine("Connecting...");
            while (true)
            {
                try
                {
                    this.client = new TcpClient()
                    {
                        SendTimeout = 100
                    };
                    await this.client.ConnectAsync(this.host, this.port, new CancellationTokenSource(100).Token);
                    Console.WriteLine("Success.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await Task.Delay(100);
                }
            }
        }

        public void Dispose()
        {
            this.client?.Close();
            this.client?.Dispose();
        }
    }
}
