#region License, Terms and Conditions
//
// GeckoConnect.cs
//
// Authors: Kori Francis <twitter.com/djbyter>
// Copyright (C) 2013 Kori Francis. All rights reserved.
// 
//  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
//
#endregion
namespace Geckonet.Core
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geckonet.Core.Models;
    using Geckonet.Core.Serialization;
    using RestSharp;
using Newtonsoft.Json;

    #endregion

    public class GeckoConnect
    {
        private static string _userAgent;
        private static string UserAgent
        {
            get
            {
                if (_userAgent == null)
                {
                    _userAgent = String.Format("Geckonet .NET RestSharp Client v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                }
                return _userAgent;
            }
        }

        public PushResult Push<T>(PushPayload<T> payload, string widgetKey)
        {
            var client = new RestClient("https://push.geckoboard.com");
            client.UserAgent = GeckoConnect.UserAgent;

            var request = new RestRequest(string.Format("v1/send/{0}", widgetKey), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(payload), ParameterType.RequestBody);
            
            var response = client.Execute<PushResult>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription);
            }

            return response.Data;
        }
    }
 
    public class GeckoException : Exception
    {
        private string s;

        public GeckoException(string s)
        {
            this.s = s;
        }
    }
 
    public class PushResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
