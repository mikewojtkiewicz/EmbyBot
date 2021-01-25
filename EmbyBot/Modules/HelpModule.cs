using Discord;
using Discord.Commands;
using EmbyBot.Data;
using EmbyBot.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("Help")]
        [Summary("Get the list of commands for this bot")]
        [Remarks("help \"init rules\"")]
        public async Task HelpAsync([Summary("The command for which to view detailed help")] string commandName = null)
        {
            CommandServiceHandler commandServiceHandler = new CommandServiceHandler();
            List<CommandInfo> commands = commandServiceHandler.GetCommandService().Commands.ToList();

            // get box prefix
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            var botPrefix = string.Empty;
            foreach (var guild in obj.Guilds)
            {
                botPrefix = guild.BotPrefix;
            }

            if (commandName == null)
            {
                EmbedAuthorBuilder author = new EmbedAuthorBuilder
                {
                    Name = Context.Client.CurrentUser.Username
                };

                EmbedBuilder embedBuilder = new EmbedBuilder
                {
                    Title = "Emby Bot Help Commands",
                    Description = $"These are a list of commands for the Emby bot. Any String field needs to have quotes \"\" around the name of the parameter. Use `{botPrefix}help \"command name\"` to view a more detailed view of the command.",
                    Author = author
                };

                foreach (CommandInfo command in commands)
                {
                    if (command.Aliases[0].Contains(" "))
                    {
                        var module = command.Aliases[0].Substring(0, command.Aliases[0].IndexOf(" "));
                    }
                    // Get the command Summary attribute information
                    string embedFieldText = command.Summary ?? "No description available\n";

                    embedBuilder.AddField($"{command.Aliases[0]}", $"{embedFieldText}");
                }

                await ReplyAsync(string.Empty, false, embedBuilder.Build());
            }
            else
            {
                CommandInfo sendCommand = null;
                foreach (CommandInfo command in commands)
                {
                    if (command.Aliases[0].ToLower() == commandName)
                    {
                        sendCommand = command;
                    }
                }

                EmbedAuthorBuilder author = new EmbedAuthorBuilder
                {
                    Name = Context.Client.CurrentUser.Username
                };

                EmbedBuilder embedBuilder = new EmbedBuilder
                {
                    Title = $"Channel Manager {sendCommand.Aliases[0]} Help",
                    Author = author
                };

                string embedFieldText = sendCommand.Summary ?? "No description available\n";
                string parameters = string.Empty;

                if (sendCommand.Parameters.Count == 0)
                    parameters = "None";
                foreach (var param in sendCommand.Parameters)
                {
                    var isOptional = string.Empty;
                    if (param.IsOptional)
                        isOptional = "Not Required";
                    else
                        isOptional = "Required";
                    parameters += $"`{param.Name}` ({param.Type.Name}, {isOptional}) - {param.Summary}\n";
                }

                embedBuilder.AddField($"{sendCommand.Aliases[0]}", $"{embedFieldText} \n\n **Parameters:**\n{parameters}\n**Example:**\n`{botPrefix}{sendCommand.Remarks}`");
                await ReplyAsync(string.Empty, false, embedBuilder.Build());
            }
        }
    }
}
