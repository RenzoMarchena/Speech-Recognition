using Newtonsoft.Json;

namespace DeepGramRecognition
{
    public class Filter
    {
        [JsonProperty("NMax")]
        public double NMax { get; set; }
        [JsonProperty("Pmin")]
        public double Pmin { get; set; }
    }
}
