﻿#region License

//   Copyright © 2009-2020 John Sheehan, Andrew Young, Alexey Zimarev and RestSharp community
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Options;
using RestSharp.Serialization;

namespace RestSharp
{
    public partial interface IRestClient
    {
        RestClientOptions ClientOptions { get; }
        
        IRestClient UseSerializer(Func<IRestSerializer> serializerFactory);

        IRestClient UseSerializer<T>() where T : IRestSerializer, new();

        IAuthenticator Authenticator { get; set; }

        Uri BaseUrl { get; set; }

        IList<Parameter> DefaultParameters { get; }


        IRestResponse<T> Deserialize<T>(IRestResponse response);

        /// <summary>
        ///     Allows to use a custom way to encode URL parameters
        /// </summary>
        /// <param name="encoder">A delegate to encode URL parameters</param>
        /// <example>client.UseUrlEncoder(s => HttpUtility.UrlEncode(s));</example>
        /// <returns></returns>
        IRestClient UseUrlEncoder(Func<string, string> encoder);

        /// <summary>
        ///     Allows to use a custom way to encode query parameters
        /// </summary>
        /// <param name="queryEncoder">A delegate to encode query parameters</param>
        /// <example>client.UseUrlEncoder((s, encoding) => HttpUtility.UrlEncode(s, encoding));</example>
        /// <returns></returns>
        IRestClient UseQueryEncoder(Func<string, Encoding, string> queryEncoder);

        IRestResponse Execute(IRestRequest request);

        IRestResponse Execute(IRestRequest request, Method httpMethod);

        IRestResponse<T> Execute<T>(IRestRequest request);

        IRestResponse<T> Execute<T>(IRestRequest request, Method httpMethod);

        byte[] DownloadData(IRestRequest request);

        byte[] DownloadData(IRestRequest request, bool throwOnError);

        Uri BuildUri(IRestRequest request);

        string BuildUriWithoutQueryParameters(IRestRequest request);

        /// <summary>
        ///     Add a delegate to apply custom configuration to HttpWebRequest before making a call
        /// </summary>
        /// <param name="configurator">Configuration delegate for HttpWebRequest</param>
        void ConfigureWebRequest(Action<HttpWebRequest> configurator);

        /// <summary>
        ///     Adds or replaces a deserializer for the specified content type
        /// </summary>
        /// <param name="contentType">Content type for which the deserializer will be replaced</param>
        /// <param name="deserializerFactory">Custom deserializer factory</param>
        void AddHandler(string contentType, Func<IDeserializer> deserializerFactory);

        /// <summary>
        ///     Removes custom deserialzier for the specified content type
        /// </summary>
        /// <param name="contentType">Content type for which deserializer needs to be removed</param>
        void RemoveHandler(string contentType);

        /// <summary>
        ///     Remove deserializers for all content types
        /// </summary>
        void ClearHandlers();

        IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod);

        IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod);

        IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod);

        IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod);

        /// <summary>
        ///     Executes the request asynchronously, authenticating if needed
        /// </summary>
        /// <typeparam name="T">Target deserialization type</typeparam>
        /// <param name="request">Request to be executed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes the request asynchronously, authenticating if needed
        /// </summary>
        /// <typeparam name="T">Target deserialization type</typeparam>
        /// <param name="request">Request to be executed</param>
        /// <param name="httpMethod">Override the request method</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, Method httpMethod, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes the request asynchronously, authenticating if needed
        /// </summary>
        /// <param name="request">Request to be executed</param>
        /// <param name="httpMethod">Override the request method</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse> ExecuteAsync(IRestRequest request, Method httpMethod, CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Executes a GET-style request asynchronously, authenticating if needed
        /// </summary>
        /// <typeparam name="T">Target deserialization type</typeparam>
        /// <param name="request">Request to be executed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse<T>> ExecuteGetAsync<T>(IRestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes a POST-style request asynchronously, authenticating if needed
        /// </summary>
        /// <typeparam name="T">Target deserialization type</typeparam>
        /// <param name="request">Request to be executed</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IRestResponse<T>> ExecutePostAsync<T>(IRestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes a GET-style asynchronously, authenticating if needed
        /// </summary>
        /// <param name="request">Request to be executed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse> ExecuteGetAsync(IRestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes a POST-style asynchronously, authenticating if needed
        /// </summary>
        /// <param name="request">Request to be executed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IRestResponse> ExecutePostAsync(IRestRequest request, CancellationToken cancellationToken = default);
    }
}