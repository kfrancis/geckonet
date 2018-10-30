using System;
using System.Collections.Generic;
using System.Data;
using Geckonet.Core;
using Geckonet.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geckonet.Tests
{
    [TestClass]
    public class DatasetTests : TestBase
    {
        private string _apiKey; // replace this value with your own

        public DatasetTests()
        {
            _apiKey = APIKEY;
        }

        [TestMethod]
        public void Live_Create_Dataset()
        {
            // Arrange
            var client = new GeckoConnect();
            var obj = new GeckoDataset()
            {
                Fields = new Dictionary<string, IDatasetField>()
                {
                    {"amount", new DatasetField(DatasetFieldType.Number, "Amount")},
                    {"timestamp", new DatasetField(DatasetFieldType.DateTime, "Date")}
                },
                UniqueBy = new List<string>() { "timestamp" }
            };
            var datasetName = $"test_{Guid.NewGuid().ToString()}";

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            var result = client.CreateDataset(obj, datasetName, _apiKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(datasetName, result.Id);
            Assert.IsTrue(DateTime.Now > result.CreatedAt);
            Assert.IsTrue(DateTime.Now > result.UpdatedAt);

            client.DeleteDataset(datasetName, _apiKey);
        }

        [TestMethod]
        public void Live_Can_Delete_Unknown_Dataset()
        {
            // Arrange
            var client = new GeckoConnect();
            var datasetName = Guid.NewGuid().ToString();

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            try
            {
                var result = client.DeleteDataset(datasetName, _apiKey);
                Assert.Fail("Expected an exception");
            }
            catch (GeckoException geckoException)
            {
                Assert.AreEqual("Dataset not found", geckoException.DatasetErrorContent.Error.Message);
            }
        }

        [TestMethod]
        public void Live_Replace_Data_In_Dataset()
        {
            // Arrange
            var client = new GeckoConnect();

            var obj = new GeckoDataset()
            {
                Fields = new Dictionary<string, IDatasetField>()
                {
                    {"amount", new DatasetField(DatasetFieldType.Number, "Amount")},
                    {"timestamp", new DatasetField(DatasetFieldType.DateTime, "Date")}
                },
                UniqueBy = new List<string>() { "timestamp" }
            };
            var datasetName = $"test_{Guid.NewGuid().ToString()}";

            // Act
            var result = client.CreateDataset(obj, datasetName, _apiKey);

            var obj2 = new GeckoDataset()
            {
                Data = new List<Dictionary<string, object>>()
                {
                    new Dictionary<string, object>()
                    {
                        { "timestamp", "2016-01-01T12:00:00Z" },
                        { "amount", 819 }
                    },
                    new Dictionary<string, object>()
                    {
                        { "timestamp", "2016-01-02T12:00:00Z" },
                        { "amount", 409 }
                    },
                    new Dictionary<string, object>()
                    {
                        { "timestamp", "2016-01-03T12:00:00Z" },
                        { "amount", 164 }
                    }
                }
            };

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            var result2 = client.UpdateDataset(obj2, datasetName, _apiKey);

            // Assert
            Assert.IsNotNull(result);

            client.DeleteDataset(datasetName, _apiKey);
        }
    }
}
