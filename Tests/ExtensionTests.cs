using Battleship2000.Logic;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ToNiceStringTests()
        {
            Version v = new();

            Assert.That(v.ToNiceString(), Is.EqualTo("0.0"));

            v = new(0, 1);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.1"));

            v = new(0, 50);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.50"));

            v = new(0, 14, 14);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.14.14"));

            v = new(0, 0, 0, 145);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.0.0.145"));

            v = new(0, 0, 14, 145);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.0.14.145"));

            v = new(0, 32, 0, 145);
            Assert.That(v.ToNiceString(), Is.EqualTo("0.32.0.145"));

            v = new(148, 50);
            Assert.That(v.ToNiceString(), Is.EqualTo("148.50"));

            v = new(148, 0);
            Assert.That(v.ToNiceString(), Is.EqualTo("148.0"));

            v = new(148, 18, 98);
            Assert.That(v.ToNiceString(), Is.EqualTo("148.18.98"));

            v = new(148, 0, 98);
            Assert.That(v.ToNiceString(), Is.EqualTo("148.0.98"));

            v = new(148, 0, 0, 145);
            Assert.That(v.ToNiceString(), Is.EqualTo("148.0.0.145"));

            v = new(414, 0, 14, 154);
            Assert.That(v.ToNiceString(), Is.EqualTo("414.0.14.154"));

            v = new(414, 148, 14, 154);
            Assert.That(v.ToNiceString(), Is.EqualTo("414.148.14.154"));

            v = new(0, 0, 0, 0);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.0"));

            v = new();
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.0"));

            v = new(0, 1);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.1"));

            v = new(0, 50);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.50"));

            v = new(0, 14, 14);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.14.14"));

            v = new(0, 0, 0, 145);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.0.0.145"));

            v = new(0, 0, 14, 145);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.0.14.145"));

            v = new(0, 32, 0, 145);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.32.0.145"));

            v = new(148, 50);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v148.50"));

            v = new(148, 0);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v148.0"));

            v = new(148, 18, 98);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v148.18.98"));

            v = new(148, 0, 98);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v148.0.98"));

            v = new(148, 0, 0, 145);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v148.0.0.145"));

            v = new(414, 0, 14, 154);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v414.0.14.154"));

            v = new(414, 148, 14, 154);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v414.148.14.154"));

            v = new(0, 0, 0, 0);
            Assert.That(v.ToNiceString(true), Is.EqualTo("v0.0"));
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
