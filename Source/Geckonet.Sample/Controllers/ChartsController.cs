using Geckonet.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Geckonet.Sample.Controllers
{
    /// <summary>
    /// Sample endpoints for Geckoboard Custom Charts
    /// </summary>
    [Authorize]
    public class ChartsController : ApiController
    {
        private Random rand = new Random();

        /// <summary>
        /// Endpoint for the number and secondary stat custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("map")]
        public HttpResponseMessage Map([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoMapPoints()
                {
                    Points = new List<MapPoint>() {
                        new MapPoint() { City = new MapCity() { CityName = "london", CountryCode = "GB" }, Size=10, Color="d8f709" },
                        new MapPoint() { City = new MapCity() { CityName = "San Francisco", CountryCode="US", RegionCode = "CA" } },
                        new MapPoint() { Latitude = "51.526263", Longitude = "-0.092429" },
                        new MapPoint() { Host = "geckoboard.com" },
                        new MapPoint() { IP = "72.38.123.170" }
                    }
                };

                return this.Request.CreateResponse<GeckoMapPoints>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the line custom chart
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("line")]
        public HttpResponseMessage Line([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoLineChart()
                {
                    Items = new List<string>() { "12.3", "2.3", "10", "15", "15", "13", "12.1", "9.8", "12.3", "2.3", "10" },
                    Settings = new GeckoLineChartSettings()
                    {
                        XAxisLabels = new List<string>() { "Jun", "Jul", "Aug" },
                        YAxisLabels = new List<string>() { "Min", "Max" },
                        Colour = "ff9900"
                    }
                };

                return this.Request.CreateResponse<GeckoLineChart>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the geckometer chart
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("geckometer")]
        public HttpResponseMessage GeckoMeter([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoMeterChart()
                {
                    Item = 23,
                    Min = new DataItem() { Value = 10, Text = "Min visitors" },
                    Max = new DataItem() { Value = 30, Text = "Max visitors" }
                };

                return this.Request.CreateResponse<GeckoMeterChart>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the funnel chart
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("funnel")]
        public HttpResponseMessage Funnel([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoFunnelChart()
                {
                    Items = new List<DataItem>()
                    {
                        new DataItem() { Value = 87809, Label = "Step1" },
                        new DataItem() { Value = 70022, Label = "Step2" },
                        new DataItem() { Value = 63232, Label = "Step3" },
                        new DataItem() { Value = 53232, Label = "Step4" },
                        new DataItem() { Value = 32123, Label = "Step5" },
                        new DataItem() { Value = 23232, Label = "Step6" },
                        new DataItem() { Value = 12232, Label = "Step7" },
                        new DataItem() { Value = 2323, Label = "Step8" },
                    }
                };

                return this.Request.CreateResponse<GeckoFunnelChart>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the bullet chart
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("bullet")]
        public HttpResponseMessage Bullet([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoBulletChart()
                {
                    Orientation = "horizontal",
                    Item = new GeckoBulletItem()
                    {
                        Label = "Revenue 2011 YTD",
                        SubLabel = "(U.S. $ in thousands)",
                        Axis = new GeckoBulletAxis()
                        {
                            Points = new List<int>() { 0, 200, 400, 600, 800, 1000 }
                        },
                        Range = new GeckoBulletRange()
                        {
                            Red = new GeckoBulletRangeItem() { Start = 0, End = 400 },
                            Amber = new GeckoBulletRangeItem() { Start = 401, End = 700 },
                            Green = new GeckoBulletRangeItem() { Start = 701, End = 1000 }
                        },
                        Measure = new GeckoBulletMeasure()
                        {
                            Current = new GeckoBulletRangeItem() { Start = 0, End = 500 },
                            Projected = new GeckoBulletRangeItem() { Start = 100, End = 900 }
                        },
                        Comparitive = new List<GeckoBulletPoint>() 
                        { 
                            new GeckoBulletPoint() { Point = 600 }
                        }
                    }
                };

                return this.Request.CreateResponse<GeckoBulletChart>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the number and secondary stat custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("highchart")]
        public HttpResponseMessage Highchart([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoHighchart()
                {
                    Chart = new HighchartChart()
                    {
                        RenderTo = "container",
                        PlotBackgroundColor = "rgba(35,37,38,0)",
                        BackgroundColor = "rgba(35,37,38,100)",
                        BorderColor = "rgba(35,37,38,100)",
                        PlotBorderColor = "rgba(35,37,38,100)",
                        PlotShadow = false,
                        Height = 170
                    },
                    Colors = new List<string>() { "#058DC7", "#50B432", "#EF561A" },
                    Credits = new HighchartCredits() { Enabled = false },
                    Title = new HighchartTitle() { Text = "test" },
                    //Tooltip = new HighchartTooltip() { Formatter = System.Web.HttpUtility.JavaScriptStringEncode("") }
                    Legend = new HighchartLegend()
                    {
                        BorderColor = "rgba(35,37,38,100)",
                        ItemWidth = 55,
                        Margin = 5,
                        Width = 200
                    },
                    PlotOptions = new HighchartPlotOptions()
                    {
                        PieOptions = new HighchartPieOptions()
                        {
                            Animation = true,
                            AllowPointSelect = true,
                            Cursor = "pointer",
                            DataLabelOptions = new HighchartDataLabelOptions() { Enabled = false },
                            ShowInLegend = true,
                            Size = "100%"
                        }
                    },
                    Series = new List<HighchartSeries>()
                    {
                        new HighchartSeries(){ 
                            Type = "pie",
                            Name = "New vs Returning",
                            Data = new List<Dictionary<string, int>>()
                            {
                                new Dictionary<string, int>(){{"Free",13491}},
                                new Dictionary<string, int>(){{"Premium",191}}
                            }
                        }
                    }

                };

                return this.Request.CreateResponse<GeckoHighchart>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
