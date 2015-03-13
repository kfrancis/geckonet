using System;
using Geckonet.Core;
using Geckonet.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Humanizer;

namespace Geckonet.Tests
{
    [TestClass]
    public class PushTests
    {
        private readonly string apiKey = "<api key here>";  // replace this value with your own

        [TestMethod]
        public void Live_Push_Monitoring()
        {
            // Arrange           
            Random.Org.Random rand = new Random.Org.Random();
            var widgetKey = "<widget key here>";        // replace this value with your own
            var obj = new GeckoMonitoring() {
                Status = rand.Next(1, 2) % 2 == 1 ? MonitoringStatus.Up : MonitoringStatus.Down,
                Downtime = DateTime.Now.AddDays(rand.Next(1, 60)).Humanize(),
                ResponseTime = string.Format("{0} ms", rand.Next(1, 1000))
            };

            var push = new PushPayload<GeckoMonitoring>()
            {
                ApiKey = this.apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", this.apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<GeckoMonitoring>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Live_Push_NumberAndSecondaryStat()
        {
            // Arrange           
            var widgetKey = "<widget key here>";        // replace this value with your own
            var obj = new NumberAndSecondaryStat() {
                DataItems = new DataItem[] {
                    new DataItem() { Text = "Visitors", Value = 4223 } ,
                    new DataItem() { Text = string.Empty, Value = 238 }
                }
            };

            var push = new PushPayload<NumberAndSecondaryStat>()
            {
                ApiKey = this.apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", this.apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<NumberAndSecondaryStat>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Can_Serialize_Push_Payload()
        {
            // Arrange
            var obj = new NumberAndSecondaryStat() {
                DataItems = new DataItem[] {
                    new DataItem() { Text = "Visitors", Value = 4223 } ,
                    new DataItem() { Text = string.Empty, Value = 238 }
                }
            };

            var push = new PushPayload<NumberAndSecondaryStat>()
            {
                ApiKey = Guid.NewGuid().ToString(),
                Data = obj
            };

            // Act
            var result = JsonConvert.SerializeObject(push, Formatting.Indented);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }
    }
}
