using Newtonsoft.Json;

namespace Spongebot.Config
{
    internal class ConfigGetter(string configFilePath = "config.json")
    {
        public string Token { get; private set; } = "";
        public string Prefix { get; private set; } = "";

        private readonly string _configFilePath = configFilePath;

        public async Task ReadJsonAsync()
        {
            try
            {
                using StreamReader sr = new(_configFilePath);
                string json = await sr.ReadToEndAsync();
                ConfigShape config = JsonConvert.DeserializeObject<ConfigShape>(json)!;
                if (!string.IsNullOrEmpty(config.Token) && !string.IsNullOrEmpty(config.Prefix))
                {
                    Token = config.Token;
                    Prefix = config.Prefix;
                }
                else
                {
                    throw new NullReferenceException("Config file is missing bot token or prefix.");
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"{_configFilePath} file not found.");
            }
            catch (JsonException)
            {
                throw new JsonException($"Exception while deserializing JSON from {_configFilePath}.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception occurred while reading {_configFilePath}: {ex.Message}", ex);
            }
        }
    }

}
