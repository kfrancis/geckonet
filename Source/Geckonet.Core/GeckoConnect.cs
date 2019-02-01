
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
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Serialization;
    using RestSharp;
    using RestSharp.Authenticators;
    #endregion

    /// <summary>
    /// A comprehensive C# API wrapper library for accessing Geckoboard.com, using XML or JSON to read/write widget data easily using strong-typed models. test
    /// </summary>
    public class GeckoConnect
    {
        private static string _userAgent;

        private static string UserAgent => _userAgent ??
                                   (_userAgent =
                                       string.Format("Geckonet .NET RestSharp Client v" +
                                                     System.Reflection.Assembly.GetExecutingAssembly().GetName().Version));

        /// <summary>
        /// Push it!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payload"></param>
        /// <param name="widgetKey"></param>
        /// <returns></returns>
        public PushResult Push<T>(PushPayload<T> payload, string widgetKey)
        {
            var client = new RestClient("https://push.geckoboard.com") { UserAgent = UserAgent };

            var request = new RestRequest($"v1/send/{widgetKey}", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(payload), ParameterType.RequestBody);

            var response = client.Execute<PushResult>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return response.Data;
        }

        /// <summary>
        /// Allows the replacement of data in a dataset
        /// </summary>
        /// <param name="payload">The dataset to send</param>
        /// <param name="name">The name of the dataset</param>
        /// <param name="apiKey">The api key</param>
        /// <returns>True if successful, throws GeckoException otherwise.</returns>
        public bool UpdateDataset(GeckoDataset payload, string name, string apiKey)
        {
            var client = new RestClient("https://api.geckoboard.com")
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, string.Empty),
                UserAgent = UserAgent
            };

            var request = new RestRequest($"datasets/{name}/data", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(payload), ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return true;
        }

        /// <summary>
        /// Deletes a dataset
        /// </summary>
        /// <param name="name">The name of the dataset to delete</param>
        /// <param name="apiKey">The api key</param>
        /// <returns>True if successful, </returns>
        public bool DeleteDataset(string name, string apiKey)
        {
            var client = new RestClient("https://api.geckoboard.com")
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, string.Empty),
                UserAgent = UserAgent
            };

            var request = new RestRequest($"datasets/{name}", Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return true;
        }

        /// <summary>
        /// Appending data to a dataset
        /// </summary>
        /// <param name="payload">The dataset payload</param>
        /// <param name="name">The name of the dataset</param>
        /// <param name="apiKey">The api key</param>
        /// <returns>True if successful</returns>
        /// <exception cref="GeckoException">Thrown if response status is not 200 OK</exception>
        public bool AppendDataset(GeckoDataset payload, string name, string apiKey)
        {
            var client = new RestClient("https://api.geckoboard.com")
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, string.Empty),
                UserAgent = UserAgent
            };

            var request = new RestRequest($"datasets/{name}/data", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(payload), ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return true;
        }

        /// <summary>
        /// Create a dataset
        /// </summary>
        /// <param name="payload">The dataset payload</param>
        /// <param name="name">The name of the dataset</param>
        /// <param name="apiKey">The api key</param>
        /// <returns>The dataset result, if applicable.</returns>
        /// <exception cref="GeckoException">Thrown if response status is not 201 CREATED</exception>
        public GeckoDatasetResult CreateDataset(GeckoDataset payload, string name, string apiKey)
        {
            var client = new RestClient("https://api.geckoboard.com")
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, string.Empty),
                UserAgent = UserAgent
            };

            var request = new RestRequest($"datasets/{name}", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(payload), ParameterType.RequestBody);

            var response = client.Execute<GeckoDatasetResult>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return response.Data;
        }

        /// <summary>
        /// Clears the dataset
        /// </summary>
        /// <param name="name">The name of the dataset</param>
        /// <param name="apiKey">The api key</param>
        /// <returns>True if successful, throws GeckoException otherwise.</returns>
        public bool ClearDataset(string name, string apiKey)
        {
            var client = new RestClient("https://api.geckoboard.com")
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, string.Empty),
                UserAgent = UserAgent
            };

            var request = new RestRequest($"datasets/{name}/data", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.JsonSerializer = new RestSharpJsonNetSerializer();
            request.AddParameter("application/json", request.JsonSerializer.Serialize(new { Data = new List<int>() }), ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new GeckoException(response.StatusDescription, response.Content);
            }

            return true;
        }
    }
}
