﻿using NetPackage.TCP;
using System;
using System.Diagnostics;
using System.Text;

namespace Battleship2000.Logic
{
    public class NetworkClient : IDisposable
    {
        private bool disposed;
        private readonly int port = 32485;
        private SimpleTcpClient client = null;

        public bool ConnectTo(string server)
        {
            this.client = new(server, this.port);
            client.Events.DataReceived += this.DataReceived;

            try
            {
                client.ConnectWithRetries(2250);
                Debug.Print(client.LocalEndpoint.Address.ToString() + "-" + client.LocalEndpoint.Port.ToString());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DataReceived(object sender, NetPackage.TCP.DataReceivedEventArgs e)
        {
            Debug.Print($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}