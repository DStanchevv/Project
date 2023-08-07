using Newtonsoft.Json;

namespace SportWave.IPInfo
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; } = null!;

        [JsonProperty("hostname")]
        public string Hostname { get; set; } = null!;

        [JsonProperty("city")]
        public string City { get; set; } = null!;

        [JsonProperty("region")]
        public string Region { get; set; } = null!;

        [JsonProperty("country")]
        public string Country { get; set; } = null!;

        [JsonProperty("loc")]
        public string Location { get; set; } = null!;

        [JsonProperty("org")]
        public string Org { get; set; } = null!;

        [JsonProperty("postal")]
        public string Postal { get; set; } = null!;

        [JsonProperty("timezone")]
        public string Timezone { get; set; } = null!;
    }
}
