using Battleship2000;
using Battleship2000.Logic;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        private ConfigurationHandler configurationHandler;
        private string testfilepath;

        [SetUp]
        public void SetUp()
        {
            this.testfilepath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "Unittestfiles", "testconfig.json"));
            this.configurationHandler = new(this.testfilepath);
        }

        [Test]
        public void TouchFileTests()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    this.configurationHandler.TouchConfigFile();
                });
                Assert.That(File.Exists(this.testfilepath), Is.True);
            });

            FileInfo fi = new(this.testfilepath);

            Assert.That(fi, Has.Length.EqualTo(0));

            byte[] buffer = null;
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    using (FileStream fs = File.Open(this.testfilepath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                    }
                });
                Assert.That(buffer, Has.Length.EqualTo(0));
            });
        }

        [Test]
        public void CreateBlankConfigTest()
        {
            Assert.DoesNotThrow(() =>
            {
                this.configurationHandler.Save();
            });

            FileInfo fi = new(this.testfilepath);

            Assert.That(fi, Has.Length.GreaterThan(0));

            byte[] buffer = null;
            string filecontent = null;
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() =>
                {
                    using (FileStream fs = File.Open(this.testfilepath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Seek(0, SeekOrigin.Begin);
                        using (StreamReader r = new(fs))
                        {
                            filecontent = r.ReadToEnd();
                        }
                    }
                });
                Assert.That(buffer, Has.Length.GreaterThan(0));
                Assert.That(filecontent, Is.Not.Null);
                Assert.That(filecontent, Is.Not.Empty);
                Assert.That(filecontent.StartsWith("{"), Is.True);
                Assert.That(filecontent.EndsWith("}"), Is.True);
            });
        }

        [Test]
        public void IsValidJsonTests()
        {
            string validjson = "{\"name\":\"John\", \"age\":30, \"car\":null}";
            string invalidjson = "{\"name\"\"John\", \"age\":30, \"car\":null}";

            Assert.Multiple(() =>
            {
                Assert.That(ConfigurationHandler.IsValidJson(validjson), Is.True);
                Assert.That(ConfigurationHandler.IsValidJson(invalidjson), Is.False);
            });
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(this.testfilepath))
            {
                File.Delete(this.testfilepath);
            }
        }
    }
}
