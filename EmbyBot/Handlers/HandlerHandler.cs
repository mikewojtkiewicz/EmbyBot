using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace EmbyBot.Handlers
{
    public class HandlerHandler
    {
        private CommandHandler _handler;
        private GuildJoinHandler _guildJoinHandler;
        private GuildLeftHandler _guildLeftHandler;
        private ReactionHandler _reactionHandler;

        public HandlerHandler(DiscordSocketClient client, IServiceProvider services)
        {
            InitHandlers(client, services);
        }

        private void InitHandlers(DiscordSocketClient client, IServiceProvider services)
        {
            // Client Handlers
            _handler = new CommandHandler(client, services);
            _guildJoinHandler = new GuildJoinHandler(client, services);
            _guildLeftHandler = new GuildLeftHandler(client);
            _reactionHandler = new ReactionHandler(client, services);
        }
    }
}
