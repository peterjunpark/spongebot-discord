using Newtonsoft.Json;

namespace Spongebot.Config
{
    internal sealed class ConfigShape
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
        [JsonProperty("prefix")]
        public string? Prefix { get; set; }
    }
}
