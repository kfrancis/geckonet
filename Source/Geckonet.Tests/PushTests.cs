using System;
using Geckonet.Core;
using Geckonet.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Geckonet.Tests
{
    [TestClass]
    public class PushTests
    {
        [TestMethod]
        public void Live_Push_Test()
        {
            // Arrange
            var apiKey = "<api key here>";              // replace this value with your own
            var widgetKey = "<widget key here>";        // replace this value with your own
            var obj = new NumberAndSecondaryStat() {
                DataItems = new DataItem[] {
                    new DataItem() { Text = "Visitors", Value = 4223 } ,
                    new DataItem() { Text = string.Empty, Value = 238 }
                }
            };

            var push = new PushPayload<NumberAndSecondaryStat>()
            {
                ApiKey = apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", apiKey);
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
