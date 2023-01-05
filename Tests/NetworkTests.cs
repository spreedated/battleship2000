using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship2000.Logic;
using System.Threading;

namespace Tests
{
    [TestFixture]
    public class NetworkTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ConnectionTests()
        {
            NetworkServer s = new("0.0.0.0");
            s.StartServer();

            Thread.Sleep(2000);

            NetworkClient n = new();

            n.ConnectTo("127.0.0.1");

            while (true)
            {
                Thread.Sleep(50);
            }
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
