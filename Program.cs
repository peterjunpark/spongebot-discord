using DSharpPlus;
using DSharpPlus.CommandsNext;
using Spongebot.Config;

namespace Spongebot;

internal class Program
{
    private static DiscordClient Client { get; set; }
    private static DiscordConfiguration Config { get; set; }
    private static CommandsNextExtension Commands { get; set; }

    private static async Task Main(string[] args)
    {
        var configGetter = new ConfigGetter("config.json");
        await configGetter.ReadJsonAsync();

        Config = new()
        {
            Intents = DiscordIntents.All,
            Token = configGetter.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        Client = new(Config);
        Client.MessageCreated += async (s, e) =>
        {
            if (e.Message is not null && e.Message.Content?.StartsWith("ping", StringComparison.CurrentCultureIgnoreCase) == true)
            {
                await e.Message.RespondAsync("pong!");
            }
        };

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    // private static async Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
    // {
    //     return Task.CompletedTask;
    // }
}
