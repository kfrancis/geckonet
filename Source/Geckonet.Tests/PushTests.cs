using System;
using System.Collections.Generic;
using Geckonet.Core;
using Geckonet.Core.Models;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Humanizer;

namespace Geckonet.Tests
{
    [TestClass]
    public class PushTests : TestBase
    {
        private string _apiKey;  // replace this value with your own

        public PushTests()
        {
            _apiKey = APIKEY;
        }

        [TestMethod]
        public void Live_Push_BarChart()
        {
            // Arrange            
            var widgetKey = BARCHART_WIDGETKEY;        // replace this value with your own
            var obj = new GeckoBarChart()
            {
                XAxis = new GeckoBarChartXAxis()
                {
                    Labels = new List<string> { "2000", "2001", "2002", "2003", "2004", "2005" }
                },
                YAxis = new GeckoBarChartYAxis()
                {
                    Format = "currency",
                    Unit = "USD"
                },
                Series = new List<GeckoBarChartSeries>()
                {
                    new GeckoBarChartSeries(){
                        Data = new List<int>{ 1000, 1500, 30600, 28800, 22300, 36900 }
                    }
                }
            };

            var push = new PushPayload<GeckoBarChart>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();
            var json = JsonConvert.SerializeObject(push, Formatting.Indented);  // Just for curiosity sake

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<GeckoBarChart>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Live_Push_RagNumbers()
        {
            // Arrange            
            var widgetKey = RAGNUMBERS_WIDGETKEY;        // replace this value with your own
            var obj = new GeckoItems()
            {
                DataItems = new DataItem[3] {
                    new DataItem() { Value = 16, Text = "Long past due" },
                    new DataItem() { Value = 64, Text = "Overdue" },
                    new DataItem() { Value = 32, Text = "Due" }
                }
            };

            var push = new PushPayload<GeckoItems>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();
            var json = JsonConvert.SerializeObject(push, Formatting.Indented);  // Just for curiosity sake

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<GeckoItems>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Live_Push_Bullet()
        {
            // Arrange            
            var widgetKey = BULLET_WIDGETKEY;        // replace this value with your own
            var obj = new GeckoBulletChart()
            {
                Orientation = "horizontal",
                Item = new GeckoBulletItem()
                {
                    Axis = new GeckoBulletAxis() { Points = new System.Collections.Generic.List<string> { "0", "50", "100", "150", "200", "250" } },
                    Comparative = new GeckoBulletPointString() { Point = "220" },
                    Label = "Revenue",
                    Measure = new GeckoBulletMeasure()
                    {
                        Current = new GeckoBulletRangeItemString()
                        {
                            End = "235",
                            Start = "0"
                        }
                    },
                    Range = new System.Collections.Generic.List<GeckoBulletRangeItem>() {
                        new GeckoBulletRangeItem(){ Color = "red", End = 125, Start = 0},
                        new GeckoBulletRangeItem(){ Color = "amber", End = 200, Start = 126},
                        new GeckoBulletRangeItem(){ Color = "green", End = 250, Start = 201}
                    },
                    SubLabel = "U.S. $ (1,000s)"
                }
            };

            var push = new PushPayload<GeckoBulletChart>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();
            var json = JsonConvert.SerializeObject(push, Formatting.Indented);  // Just for curiosity sake

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<GeckoBulletChart>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Live_Push_Monitoring()
        {
            // Arrange           
            var rand = new Random();
            var widgetKey = MONITORING_WIDGETKEY;        // replace this value with your own
            var obj = new GeckoMonitoring()
            {
                Status = rand.Next(1, 2) % 2 == 1 ? MonitoringStatus.Up : MonitoringStatus.Down,
                Downtime = DateTime.Now.AddDays(rand.Next(1, 60)).Humanize(),
                ResponseTime = string.Format("{0} ms", rand.Next(1, 1000))
            };

            var push = new PushPayload<GeckoMonitoring>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
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
            var widgetKey = NUMBERANDSTAT_WIDGETKEY;        // replace this value with your own
            var obj = new NumberAndSecondaryStat()
            {
                DataItems = new DataItem[] {
                    new DataItem() { Text = "Visitors", Value = 4223 } ,
                    new DataItem() { Text = string.Empty, Value = 238 }
                }
            };

            var push = new PushPayload<NumberAndSecondaryStat>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<NumberAndSecondaryStat>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Live_Push_Geckometer()
        {
            // Arrange           
            var widgetKey = GECKOMETER_WIDGETKEY;        // replace this value with your own
            var obj = new GeckoMeterChart()
            {
                Item = 0.9m,
                Format = "percent",
                Min = new DataItem() { Text = "Min", Value = 0.0m },
                Max = new DataItem() { Text = "Max", Value = 1.0m }
            };

            var push = new PushPayload<GeckoMeterChart>()
            {
                ApiKey = _apiKey,
                Data = obj
            };
            var client = new GeckoConnect();

            // Act
            Assert.AreNotEqual("<api key here>", _apiKey);
            Assert.AreNotEqual("<widget key here>", widgetKey);
            var result = client.Push<GeckoMeterChart>(push, widgetKey);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void Can_See_Error_Messages()
        {
            // Arrange
            try
            {
                var client = new GeckoConnect();
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
                var result = client.Push<NumberAndSecondaryStat>(push, Guid.NewGuid().ToString());
            }
            catch (GeckoException gEx)
            {
                // Assert
                Assert.IsNotNull(gEx);
                Assert.IsNotNull(gEx.PushErrorContent);
                Assert.IsTrue(!string.IsNullOrEmpty(gEx.PushErrorContent.Error) || !string.IsNullOrEmpty(gEx.PushErrorContent.Message));
            }
        }
    }
}
