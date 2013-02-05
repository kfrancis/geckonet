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
        public HttpResponseMessage NumberAndSecondaryStat([FromUri]string format = "JSON", [FromUri]string type = "1")
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
                        new MapPoint() { City = new MapCity() { CityName = "london", CountryCode = "GB" }, Size=8, Color="d8f709", CssClass="mycss" },
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
    }
}
