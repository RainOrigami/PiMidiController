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

        public event EventHandler<(CPUCore[] cpus, MemoryStatus memoryStatus)>? HardwareInfoReceived;
        public event EventHandler<PageDefinition[]>? PageDefinitionsReceived;
        public event EventHandler<(Guid controlId, int value)>? ValueReceived;
        public event EventHandler<(Guid controlId, int red, int green, int blue)>? ColorReceived;
        public event EventHandler? Disconnected;

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
                NetworkStream stream = this.client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(raw);
                byte[] frameSize = BitConverter.GetBytes(buffer.Length);
                await stream.WriteAsync(frameSize, 0, frameSize.Length);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
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

            while (true)
            {
                try
                {
                    NetworkStream stream = this.client.GetStream();

                    byte[] buffer = new byte[1024 * 1024 * 52];
                    await stream.ReadAsync(buffer, 0, 4);
                    int frameSize = BitConverter.ToInt32(buffer, 0);
                    int readSize = 0;

                    while (readSize < frameSize)
                    {
                        while (!stream.DataAvailable)
                        {
                            await Task.Delay(100);
                        }
                        readSize += await stream.ReadAsync(buffer, readSize, frameSize - readSize);
                    }

                    string receivedData = Encoding.UTF8.GetString(buffer, 0, readSize);
                    MessageData? messageData = JsonConvert.DeserializeObject<MessageData>(receivedData, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                    if (messageData == null)
                    {
                        throw new Exception("Received invalid message data");
                    }

                    switch (messageData)
                    {
                        case SysInfoMessageData sysInfoMessageData:
                            this.HardwareInfoReceived?.Invoke(this, (sysInfoMessageData.Cpus, sysInfoMessageData.MemoryStatus));
                            continue;
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
                    try
                    {
                        this.client.Close();
                    }
                    catch { }
                    this.Disconnected?.Invoke(this, EventArgs.Empty);
                    await connect();
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
                    await this.client.ConnectAsync(this.host, this.port, new CancellationTokenSource(500).Token);
                    Console.WriteLine("Success.");
                    break;
                }
                catch (OperationCanceledException)
                {
                    await Task.Delay(500);
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
