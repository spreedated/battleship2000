using NetPackage.TCP;
using NetworkLayer.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLayer.Logic
{
    public class NetworkServer : IDisposable
    {
        private bool disposed;
        private readonly uint port;
        private SimpleTcpServer server = null;
        private readonly string bindingInterface = null;

        public event EventHandler BsClientConnected;

        public NwoConnectedClient ConnectedClient { get; private set; }

        public NetworkServer(string bindingInterface, uint port = 32485)
        {
            this.bindingInterface = bindingInterface;
            this.port = port;
        }

        public bool StartServer()
        {
            this.server = new(this.bindingInterface, (int)this.port);
            server.Events.DataReceived += this.DataReceived;
            server.Events.ClientDisconnected += this.ClientDisconnected;
            server.Events.ClientConnected += this.ClientConnected;

            try
            {
                server.Start();
                //Log.Information($"Server running, bound to \"{this.bindingInterface}:{this.port}\"");
            }
            catch (Exception ex)
            {
                //Log.Error(ex, $"Server start error  - ");
                return false;
            }

            return true;
        }

        public bool StopServer()
        {
            try
            {
                this.server.Stop();
                foreach (string c in server.GetClients())
                {
                    try
                    {
                        this.server.DisconnectClient(c);
                        //Log.Information($"Client disconnected \"{c}\"");
                    }
                    catch (Exception ex)
                    {
                        //Log.Error(ex, $"Client disconnection error  - ");
                    }
                }
                this.server.Dispose();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Task<bool> StartServerAsync()
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                return this.StartServer();
            });
        }

        public Task<bool> StopServerAsync()
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                this.ConnectedClient = null;
                return this.StopServer();
            });
        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

            //Log.Verbose($"Server received -> [{e.IpPort}] {data}");

            if (this.ConnectedClient == null)
            {
                if (!WaitingOnConnectionDataProcess(data, e))
                {
                    return;
                }
            }

            if (this.ConnectedClient != null)
            {

            }
        }

        private bool WaitingOnConnectionDataProcess(string data, DataReceivedEventArgs e)
        {
            NwoClientConnected n = null;

            try
            {
                n = JsonConvert.DeserializeObject<NwoClientConnected>(data);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, $"Message parsing error");
                return false;
            }

            bool[] conditions = new bool[2];

            if (n.Version == typeof(NetworkServer).Assembly.GetName().Version)
            {
                conditions[1] = true;
            }
            else
            {
                //Log.Debug($"Client version mismatch \"{n.Version}\"");
            }

            if (conditions.Any(x => !x))
            {
                //Log.Warning($"Client sent wrong information");
                return false;
            }

            //Log.Information($"Client connected! - Playername \"{n.Playername}\"");

            this.ConnectedClient = new()
            {
                IpPort = e.IpPort,
                Playername = n.Playername,
                Version = n.Version
            };

            this.BsClientConnected?.Invoke(this, EventArgs.Empty);

            return true;
        }

        private void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            //Log.Verbose($"Client ({e.IpPort}) disconnected - reason: \"{e.Reason}\"");
        }

        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            //Log.Verbose($"Client ({e.IpPort}) connected - reason: \"{e.Reason}\"");

            NwoClientConnected n = new()
            {
                //TODO: set player name as DI
                //Playername = RuntimeStorage.Config.Player.Playername,
                Version = typeof(NetworkServer).Assembly.GetName().Version
            };

            this.server.Send(e.IpPort, n.Json);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.server?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
