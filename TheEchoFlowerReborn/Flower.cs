using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.Webhook;
using Discord.WebSocket;
namespace TheEchoFlowerReborn
{

    public class Flower
    {
        private static DiscordSocketClient _client;
        private static CommandService _commandService;
        public static async Task Main(string token)
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            });
            _client.Log += Logger.Log;
            _client.MessageReceived += MessageReceived;
            _client.MessageUpdated += MessageUpdated;


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static async Task MessageUpdated(Cacheable<IMessage, ulong> cacheable, SocketMessage message, ISocketMessageChannel channel)
        {
            Logger.Log("Somethink has changed");
        }

        private static async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsWebhook || message.Author.IsBot) { return; }
            Logger.Log($"<{message.Author.Username}> {message.Content}", "Message");

            try
            {
                IIntegrationChannel aschannel = _client.GetChannel(message.Channel.Id) as IIntegrationChannel;
                if (aschannel == null) { throw new Exception($"Fuck you {message.Author.GlobalName}"); }
                IWebhook a;
                
                if (!aschannel.GetWebhooksAsync().Result.Any(x => x.Name == "EchoFlower"))
                {
                    a = aschannel.CreateWebhookAsync("EchoFlower").Result;
                }
                else
                {
                    a = aschannel.GetWebhooksAsync().Result.First(x => x.Name == "EchoFlower");
                }
                DiscordWebhookClient webhook = new DiscordWebhookClient(a);

                if (message.Thread != null)
                {
                    await webhook.SendMessageAsync(message.Content, false, message.Embeds, message.Author.GlobalName, message.Author.GetAvatarUrl(), null, null, null, MessageFlags.None, message.Thread.Id, message.Thread.Name, null, null);
                }
                else
                {
                    await webhook.SendMessageAsync(message.Content, false, message.Embeds, message.Author.GlobalName, message.Author.GetAvatarUrl());
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(ex);
                await message.Channel.SendMessageAsync(ex.Message);
            }


        }
    }
}