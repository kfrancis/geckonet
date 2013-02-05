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
