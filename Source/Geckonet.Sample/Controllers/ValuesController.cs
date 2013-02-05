using Geckonet.Core.Models;
using System;
using System.Collections.Generic;
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
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("User not authorized.")); }
                if (int.Parse(type) != 1) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new NumberAndSecondaryStat();
                var items = new List<DataItem>() { 
                    new DataItem(){ text = "test1", value = rand.Next(100) },
                    new DataItem(){ text = "test2", value = rand.Next(100) }
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
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("User not authorized.")); }
                //if (int.Parse(type) != 3) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("type '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new RAGNumbersOnly();
                var items = new List<DataItem>() { 
                    new DataItem(){ text = "test1", value = rand.Next(100) },
                    new DataItem(){ text = "test2", value = rand.Next(100) },
                    new DataItem(){ text = "test3", value = rand.Next(100) }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<RAGNumbersOnly>(HttpStatusCode.OK, retVal);
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
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("User not authorized.")); }
                if (int.Parse(type) != 2) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException(string.Format("Type parameter '{0}' is wrong.", type))); }

                // You would modify what gets returned here to make it meaningful 
                var retVal = new RAGNumbersOnly();
                var items = new List<DataItem>() { 
                    new DataItem(){ text = "test1", value = rand.Next(100), prefix = "$" },
                    new DataItem(){ text = "test2", value = rand.Next(100), prefix = "$" },
                    new DataItem(){ text = "test3", value = rand.Next(100), prefix = "$" }
                };
                retVal.DataItems = items.ToArray();

                return this.Request.CreateResponse<RAGNumbersOnly>(HttpStatusCode.OK, retVal);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}