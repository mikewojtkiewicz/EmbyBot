using Discord;
using Discord.WebSocket;
using EmbyBot.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Handlers
{
    public class GuildLeftHandler
    {
        private DiscordSocketClient _client;

        public GuildLeftHandler(DiscordSocketClient client)
        {
            _client = client;

            _client.LeftGuild += HandleLeftGuild;
        }

        private async Task HandleLeftGuild(SocketGuild guild)
        {
            Console.WriteLine($"Left {guild.Name}");
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();

            foreach (var guildObject in obj.Guilds)
            {
                if (guildObject.Id == guild.Id)
                {
                    obj.Guilds.Remove(guildObject);
                    break;
                }
            }

            rootObject.SerializeData<Guild>(obj);
        }
    }
}
