using NetPackage.TCP;
using Serilog;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship2000.Logic
{
    public class NetworkServer : IDisposable
    {
        private bool disposed;
        private readonly uint port;
        private SimpleTcpServer server = null;
        private readonly string bindingInterface = null;

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
                Log.Information($"[NetworkServer] Server running, bound to \"{this.bindingInterface}:{this.port}\"");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[NetworkServer] Server start error  - ");
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
                        Log.Information($"[NetworkServer] Client disconnected \"{c}\"");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"[NetworkServer] Client disconnection error  - ");
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
                return this.StopServer();
            });
        }

        private void DataReceived(object sender, NetPackage.TCP.DataReceivedEventArgs e)
        {
            Log.Verbose($"[NetworkServer] Server received -> [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }

        private void ClientDisconnected(object sender, NetPackage.TCP.ConnectionEventArgs e)
        {
            Log.Verbose($"[NetworkServer] Client ({e.IpPort}) disconnected - reason: \"{e.Reason}\"");
        }

        private void ClientConnected(object sender, NetPackage.TCP.ConnectionEventArgs e)
        {
            Log.Verbose($"[NetworkServer] Client ({e.IpPort}) connected - reason: \"{e.Reason}\"");
            this.server.Send(e.IpPort, "Welcome to Battleship 2000");
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
