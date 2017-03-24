using Newtonsoft.Json;

namespace DeepGramRecognition
{
    public class Query
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("userID")]
        public string UserID { get; set; }
        [JsonProperty("contentID")]
        public string ContentID { get; set; }
        [JsonProperty("query")]
        public string QueryText { get; set; }
        [JsonProperty("snippet")]
        public bool Snippet { get; set; }
        [JsonProperty("filter")]
        public Filter Filter { get; set; }
        [JsonProperty("sort")]
        public string Sort { get; set; }

        
    }
}
