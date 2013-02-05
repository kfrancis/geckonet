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
    [Authorize]
    public class ValuesController : ApiController
    {
        private Random rand = new Random();

        /// <summary>
        /// Endpoint for the number and secondary stat custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("numberandsecondarystat")]
        public HttpResponseMessage NumberAndSecondaryStat([FromUri]string format = "JSON", [FromUri]string type = "1")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new NumberAndSecondaryStat();
                var items = new List<DataItem>() { 
                    new DataItem(){ Text = "test1", Value = rand.Next(100) },
                    new DataItem(){ Text = "test2", Value = rand.Next(100) }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<NumberAndSecondaryStat>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the RAG (Numbers Only) custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("ragnumbersonly")]
        public HttpResponseMessage RAGNumbersOnly([FromUri]string format = "JSON", [FromUri]string type = "3")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 3) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("type '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoItems();
                var items = new List<DataItem>() { 
                    new DataItem(){ Text = "test1", Value = rand.Next(100) },
                    new DataItem(){ Text = "test2", Value = rand.Next(100) },
                    new DataItem(){ Text = "test3", Value = rand.Next(100) }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<GeckoItems>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the RAG Columns and Numbers custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("ragcolumnandnumbers")]
        public HttpResponseMessage RAGColumnAndNumbers([FromUri]string format = "JSON", [FromUri]string type = "2")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 2) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoItems();
                var items = new List<DataItem>() { 
                    new DataItem(){ Text = "test1", Value = rand.Next(100), Prefix = "$" },
                    new DataItem(){ Text = "test2", Value = rand.Next(100), Prefix = "$" },
                    new DataItem(){ Text = "test3", Value = rand.Next(100), Prefix = "$" }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<GeckoItems>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Endpoint for the text custom widget
        /// </summary>
        /// <param name="format">The format for the return, since not configurable, is just set by default.</param>
        /// <param name="type">The type of widget as defined on their documentation</param>
        /// <returns>A response, containing random information if authorized.</returns>
        [HttpGet]
        [ActionName("text")]
        public HttpResponseMessage Text([FromUri]string format = "JSON", [FromUri]string type = "4")
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("API key not specified.")); }
                if (Guid.Parse(ConfigurationManager.AppSettings["GeckoAPIkey"]).ToString() != User.Identity.Name) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception(string.Format("Unknown API key, should be '{0}'.", ConfigurationManager.AppSettings["GeckoAPIkey"]))); }
                //if (int.Parse(type) != 4) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new GeckoItems();
                var items = new List<DataItem>() { 
                    new DataItem(){ Text = NLipsum.Core.LipsumGenerator.Generate(rand.Next(1, 2)), Type = DataItemType.None },
                    new DataItem(){ Text = NLipsum.Core.LipsumGenerator.Generate(rand.Next(1, 2)), Type = DataItemType.Info },
                    new DataItem(){ Text = NLipsum.Core.LipsumGenerator.Generate(rand.Next(1, 2)), Type = DataItemType.Alert }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<GeckoItems>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}