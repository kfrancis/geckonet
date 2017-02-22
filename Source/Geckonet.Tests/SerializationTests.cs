using System;
using System.Collections.Generic;
using Geckonet.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Geckonet.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void Can_Serialize_Widget()
        {
            var obj = new GeckoList()
            {
                new DataListItem()
                {
                    Title = new DataListItemTitle() { Text = "Chrome" },
                    Label = new DataListItemLabel() { Name = "New!", Color = "#ff2015" },
                    Description = "40327 visits"
                },
                new DataListItem()
                {
                    Title = new DataListItemTitle() { Text = "Safari" },
                    Description = "11577 visits"
                },
                new DataListItem()
                {
                    Title = new DataListItemTitle() { Text = "Firefox" },
                    Description = "10296 visits"
                },
                new DataListItem()
                {
                    Title = new DataListItemTitle() { Text = "Internet Explorer" },
                    Description = "3587 visits"
                },
                new DataListItem()
                {
                    Title = new DataListItemTitle() { Text = "Opera" },
                    Description = "499 visits"
                }
            };

            // Act
            var result = JsonConvert.SerializeObject(obj, Formatting.Indented);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Can_Serialize_Dataset()
        {
            var obj = new GeckoDataset()
            {
                Fields = new Dictionary<string, IDatasetField>()
                {
                    {"amount", new DatasetField(DatasetFieldType.number, "Amount")},
                    {"timestamp", new DatasetField(DatasetFieldType.datetime, "Date")}
                },
                UniqueBy = new List<string>() { "timestamp" }
            };

            // Act
            var result = JsonConvert.SerializeObject(obj, Formatting.Indented);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Can_Serialize_Push_Payload()
        {
            // Arrange
            var obj = new NumberAndSecondaryStat()
            {
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
