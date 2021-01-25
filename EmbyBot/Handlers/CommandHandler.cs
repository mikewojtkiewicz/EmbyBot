using Discord;
using Discord.Commands;
using Discord.WebSocket;
using EmbyBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Handlers
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public CommandHandler(DiscordSocketClient client, IServiceProvider services = null)
        {
            _client = client;
            _services = services;

            CommandServiceHandler commandServiceHandler = new CommandServiceHandler();
            _commands = commandServiceHandler.GetCommandService(services);

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            SocketUserMessage msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;

            // get box prefix
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            var botPrefix = string.Empty;
            foreach (var guild in obj.Guilds)
            {
                if (context.Guild != null)
                {
                    if (guild.Id == context.Guild.Id)
                    {
                        botPrefix = guild.BotPrefix;
                    }
                }
            }


            if (msg != null)
            {
                if (msg.HasStringPrefix(botPrefix, ref argPos) || msg.HasMentionPrefix((IUser)context.Client.CurrentUser, ref argPos))
                {
                    var result = await _commands.ExecuteAsync(context, argPos, _services);
                    if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    {
                        await context.Channel.SendMessageAsync(result.ErrorReason);
                        Console.WriteLine($"{DateTime.Now}");
                        Console.WriteLine($"\t{result.Error}\n\t{result.ErrorReason}");
                    }
                }
            }
        }
    }
}
