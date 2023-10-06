using NetPackage.TCP;
using System;
using System.Diagnostics;
using System.Text;

namespace NetworkLayer.Logic
{
    public class NetworkClient : IDisposable
    {
        private bool disposed;
        private readonly int port = 32485;
        private SimpleTcpClient client = null;

        public bool IsConnected
        {
            get
            {
                if (this.client != null)
                {
                    return this.client.IsConnected;
                }
                return false;
            }
        }

        public void ConnectTo(string server)
        {
            this.client = new(server, this.port);
            client.Events.DataReceived += this.DataReceived;

            client.ConnectWithRetries(2250);
            Debug.Print(client.LocalEndpoint.Address.ToString() + "-" + client.LocalEndpoint.Port.ToString());
        }

        private void DataReceived(object sender, NetPackage.TCP.DataReceivedEventArgs e)
        {
            Debug.Print($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
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
                    this.client?.Dispose();
                }

                disposed = true;
            }
        }
    }
}
