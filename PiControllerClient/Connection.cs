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

        //private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public event EventHandler<PageDefinition[]>? PageDefinitionsReceived;
        public event EventHandler<(Guid controlId, int value)>? ValueReceived;

        public Connection(string host, int port)
        {
            this.host = host;
            this.port = port;

            Task.Run(handle);
            //new Thread(new ThreadStart(handle)).Start();
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
            catch
            {
                await connect();
                await Send(raw);
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

            //while (!cancellationTokenSource.Token.IsCancellationRequested)
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
                    Console.WriteLine("Data received.");

                    try
                    {
                        PageDefinition[]? definitions = JsonConvert.DeserializeObject<PageDefinition[]>(receivedData, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                        if (definitions != null)
                        {
                            Console.WriteLine("Received new definition");
                            this.PageDefinitionsReceived?.Invoke(this, definitions);
                            continue;
                        }
                    }
                    catch { }

                    try
                    {
                        Guid controlId = Guid.Empty;
                        (controlId, int value) = JsonConvert.DeserializeObject<(Guid, int)>(receivedData);
                        if (controlId != Guid.Empty)
                        {
                            this.ValueReceived?.Invoke(this, (controlId, value));
                        }
                    }
                    catch { }

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
            Console.WriteLine("Connect called from");
            Console.WriteLine(Environment.StackTrace);
            //while (!cancellationTokenSource.Token.IsCancellationRequested)
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
                    Thread.Sleep(100);
                }
            }
        }

        public void Dispose()
        {
            //this.cancellationTokenSource.Cancel();
            this.client?.Close();
            this.client?.Dispose();
            //this.cancellationTokenSource.Dispose();
        }
    }
}
