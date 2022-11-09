using Newtonsoft.Json;

namespace Geckonet.Core.Models
{
    /// <summary>
    /// The result
    /// </summary>
    public class PushResult
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}