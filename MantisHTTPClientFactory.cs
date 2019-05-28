using System.Net.Http;

namespace MantisBTRestAPIClient
{
    /// <summary>
    /// MantisBT REST API client factory
    /// </summary>
    public static class MantisHTTPClientFactory
    {
        /// <summary>
        /// build a new client for MantisBT REST API
        /// </summary>
        /// <param name="baseUrl">base url</param>
        /// <param name="token">security token</param>
        /// <returns>a value tuple with both a new client for the MantisBT Rest API and a new HTTPClient linked to it</returns>
        public static (MantisHTTPClient client,HttpClient httpClient) New(
            string baseUrl,
            string token
            )
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            return (new MantisHTTPClient(baseUrl, httpClient), httpClient);
        }

        /// <summary>
        /// build a new client for MantisBT REST API
        /// </summary>
        /// <param name="baseUrl">base url</param>
        /// <param name="token">security token</param>
        /// <param name="httpClient">http client</param>
        /// <returns></returns>
        public static MantisHTTPClient New(
            string baseUrl,
            string token,
            HttpClient httpClient
            )
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            return new MantisHTTPClient(baseUrl, httpClient);
        }
    }
}
