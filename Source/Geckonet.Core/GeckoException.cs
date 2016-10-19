using System;
using Newtonsoft.Json;

namespace Geckonet.Core
{
    /// <summary>
    /// An exception coming from Geckoboard
    /// </summary>
    public class GeckoException : Exception
    {
        /// <summary>
        /// The http status result
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// The content returned (error messages) from the failed push API call
        /// </summary>
        public GeckoPushExceptionContent PushErrorContent { get; private set; }

        /// <summary>
        /// The content returned (error messages) from the failed dataset API call
        /// </summary>
        public GeckoDatasetExceptionContent DatasetErrorContent { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The http status result</param>
        public GeckoException(string status)
            : this(status, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The http status of the response</param>
        /// <param name="content">The json error content returned in the body of the response</param>
        public GeckoException(string status, string content)
        {
            Status = status;
            try
            {
                PushErrorContent = JsonConvert.DeserializeObject<GeckoPushExceptionContent>(content);
            }
            catch
            {
                // ignored
            }
            try
            {
                DatasetErrorContent = JsonConvert.DeserializeObject<GeckoDatasetExceptionContent>(content);
            }
            catch
            {
                // ignored
            }
        }
    }

    public class GeckoDatasetExceptionContent
    {
        [JsonProperty("error")]
        public GeckoDatasetError Error { get; set; }
    }

    public class GeckoDatasetError
    {
        /// <summary>
        /// The message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// The content of the error
    /// </summary>
    public class GeckoPushExceptionContent
    {
        /// <summary>
        /// The message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// The error
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}