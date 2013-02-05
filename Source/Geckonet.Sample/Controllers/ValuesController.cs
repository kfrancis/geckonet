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

        [HttpGet]
        [ActionName("numberandsecondarystat")]
        public HttpResponseMessage NumberAndSecondaryStat()
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("User not authorized.")); }

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

        [HttpGet]
        [ActionName("ragnumbersonly")]
        public HttpResponseMessage RAGNumbersOnly()
        {
            try
            {
                if (!User.Identity.IsAuthenticated) { return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("User not authorized.")); }

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
    }
}