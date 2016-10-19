using System;
using System.Collections.Generic;
using System.Data;
using Geckonet.Core;
using Geckonet.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geckonet.Tests
{
    [TestClass]
    public class DatasetTests
    {
        private readonly string apiKey = "<api key here>"; // replace this value with your own

        [TestMethod]
        public void Live_Create_Dataset()
        {
            // Arrange
            var client = new GeckoConnect();
            var obj = new GeckoDataset()
            {
                Fields = new Dictionary<string, IDatasetField>()
                {
                    {"amount", new DatasetField(DatasetFieldType.number, "Amount")},
                    {"timestamp", new DatasetField(DatasetFieldType.datetime, "Date")}
                },
                UniqueBy = new List<string>() { "timestamp" }
            };
            var datasetName = $"test_{Guid.NewGuid().ToString()}";

            // Act
            Assert.AreNotEqual("<api key here>", this.apiKey);
            var result = client.CreateDataset(obj, datasetName, this.apiKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(datasetName, result.Id);
            Assert.IsTrue(DateTime.Now > result.CreatedAt);
            Assert.IsTrue(DateTime.Now > result.UpdatedAt);

            client.DeleteDataset(datasetName, this.apiKey);
        }

        [TestMethod]
        public void Live_Can_Delete_Unknown_Dataset()
        {
            // Arrange
            var client = new GeckoConnect();
            var datasetName = Guid.NewGuid().ToString();

            // Act
            Assert.AreNotEqual("<api key here>", this.apiKey);
            try
            {
                var result = client.DeleteDataset(datasetName, this.apiKey);
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
            var datasetName = "test";

            // Act
            Assert.AreNotEqual("<api key here>", this.apiKey);
            var result = client.UpdateDataset(obj, datasetName, this.apiKey);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
