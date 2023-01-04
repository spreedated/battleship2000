using NetPackage.TCP;
using Serilog;
using System;
using System.Diagnostics;
using System.Text;

namespace Battleship2000.Logic
{
    public class NetworkServer : IDisposable
    {
        private bool disposed;
        private readonly int port = 32485;
        private SimpleTcpServer server = null;

        public bool StartServer()
        {
            this.server = new("0.0.0.0", this.port);
            server.Events.DataReceived += this.DataReceived;
            server.Events.ClientDisconnected += this.ClientDisconnected;
            server.Events.ClientConnected += this.ClientConnected;

            try
            {
                server.Start();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DataReceived(object sender, NetPackage.TCP.DataReceivedEventArgs e)
        {
            Debug.Print($"Server -> [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
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
