using Newtonsoft.Json;

namespace Utility {
    public class Http {
        /// <summary>
        /// The global HttpClient to be used.
        /// </summary>
        public static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Fetches the specified resource from JioSaavn.
        /// </summary>
        ///
        /// <typeparam name="T"> The response type of the resource </typeparam>
        /// <param name="type"> The type of resource to fetch </param>
        /// <param name="id"> The id of the resource to fetch </param>
        /// <returns></returns>
        public static T FetchResource<T>(Saavn.ResourceType type , string id) {
            // The base URL to JioSaavn API
            string baseUrl = "https://www.jiosaavn.com/api.php";

            // Paramters common to all requests
            string commonParams = "api_version=4&_format=json&ctx=web6dot0&_marker=0";

            // Resource-specific paramters
            string customParams;

            // Initializing custom paramters based on resource type
            if (type == Saavn.ResourceType.MUSIC) {
                customParams = $"__call=webapi.get&token={id}&type=song&includeMetaTags=0";
            } else if (type == Saavn.ResourceType.PLAYLIST) {
                customParams = $"__call=webapi.get&token={id}&type=playlist&includeMetaTags=0&n=9999999999";
            } else {
                customParams = $"__call=song.generateAuthToken&url={Uri.EscapeDataString(id)}&bitrate=128";
            }

            // The full URL
            string fullUrl = $"{baseUrl}?{customParams}&{commonParams}";

            // Fetching the resource
            string response = client.GetAsync(fullUrl).Result.Content.ReadAsStringAsync().Result;

            // Deserializing the response and returning it
            return JsonConvert.DeserializeObject<T>(response)!;
        }
    }

}
