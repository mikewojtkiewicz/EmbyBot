using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EmbyBot.Handlers
{
    public class CommandServiceHandler
    {
        public CommandService GetCommandService(IServiceProvider services = null)
        {
            CommandService command = new CommandService();
            command.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            return command;
        }
    }
}
