using Discord.WebSocket;
using EmbyBot.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Handlers
{
    public class GuildJoinHandler
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        public GuildJoinHandler(DiscordSocketClient client, IServiceProvider services)
        {
            _client = client;
            _services = services;

            _client.JoinedGuild += HandleJoinGuild;
        }

        private async Task HandleJoinGuild(SocketGuild guild)
        {
            Console.WriteLine($"Joined {guild.Name}");

            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();

            // see if guild already exists
            bool createGuild = true;
            List<Guild> guilds;
            if (obj.Guilds != null)
            {
                guilds = obj.Guilds;
                foreach (var guidObject in obj.Guilds)
                {
                    if (guidObject.Id == guild.Id)
                    {
                        createGuild = false;
                    }
                }
            }
            else
            {
                guilds = new List<Guild>();
            }

            if (createGuild)
            {
                Guild newGuild = new Guild
                {
                    Id = guild.Id,
                    Name = guild.Name,
                    BotPrefix = "!"
                };

                guilds.Add(newGuild);
                obj.Guilds = guilds;
                rootObject.SerializeData<Guild>(obj);
            }
        }
    }
}
