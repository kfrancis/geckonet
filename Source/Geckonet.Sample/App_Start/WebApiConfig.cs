using Geckonet.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Geckonet.Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            config.EnableQuerySupport();

            var xml = config.Formatters.XmlFormatter;
            xml.Indent = true;
            xml.UseXmlSerializer = true;

            var json = config.Formatters.JsonFormatter;
            json.Indent = true;

            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthMessageHandler() { PrincipalProvider = new GuidAPIKeyPrincipalProvider() });

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            TraceConfig.Register(config);
        }
    }
}
