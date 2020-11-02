using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Lavalink4NET;
using Lavalink4NET.DiscordNet;
using Lavalink4NET.Player;

const GatewayIntents Intents = GatewayIntents.Guilds | GatewayIntents.GuildVoiceStates | GatewayIntents.GuildMembers;

using var client = new DiscordSocketClient(new DiscordSocketConfig { GatewayIntents = Intents });
await client.LoginAsync(TokenType.Bot, "...");
await client.StartAsync();

using var node = new LavalinkNode(new LavalinkNodeOptions(), new DiscordClientWrapper(client));
await node.InitializeAsync();

client.Ready += () =>
{
    _ = Task.Run(async () =>
    {
        var player = await node.JoinAsync<LavalinkPlayer>(123, 123);
        await player.PlayAsync(await node.GetTrackAsync("https://www.youtube.com/watch?v=..."));
    });

    return Task.CompletedTask;
};

await Task.Delay(-1);
