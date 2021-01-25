using Discord;
using Discord.Commands;
using EmbyBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Modules
{
    [Group("set")]
    [RequireUserPermission(GuildPermission.Administrator, Group = "Administrator")]
    [RequireOwner(Group = "Administrator")]
    public class SetModule: ModuleBase<SocketCommandContext>
    {
        [Command("prefix")]
        [Summary("Sets the bot prefix")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Remarks("set prefix !")]
        public async Task SetPrefixAsync([Summary("The new prefix for the bot")] string newPrefix)
        {
            var contextGuild = Context.Guild;
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            Guild guildObject = null;
            foreach (var guild in obj.Guilds)
            {
                if (guild.Id == contextGuild.Id)
                {
                    guildObject = guild;
                }
            }
            guildObject.BotPrefix = newPrefix;

            rootObject.SerializeData<Guild>(obj);

            await Context.Channel.SendMessageAsync($"Set bot prefix to {newPrefix}");
            Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine($"\t{contextGuild.Name} set a new prefix to {newPrefix}");
        }

        [Command("embyurl")]
        [Summary("Sets the URL For Emby")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Remarks("set embyurl https://192.168.1.1:8096")]
        public async Task SetEmbyUrlAsync([Summary("The URL string")] string urlString)
        {
            var contextGuild = Context.Guild;
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            Guild guildObject = null;
            foreach (var guild in obj.Guilds)
            {
                if (guild.Id == contextGuild.Id)
                {
                    guildObject = guild;
                }
            }
            if(guildObject != null)
            {
                EmbyData embyData;
                if(guildObject.EmbyData != null)
                {
                    embyData = guildObject.EmbyData;
                    embyData.EmbyUrl = urlString;
                }
                else
                {
                    embyData = new EmbyData
                    {
                        EmbyUrl = urlString
                    };
                }

                guildObject.EmbyData = embyData;

                rootObject.SerializeData<Guild>(obj);

                await Context.Channel.SendMessageAsync($"Set Emby URL to {urlString}");
                Console.WriteLine($"{DateTime.Now}");
                Console.WriteLine($"\t{contextGuild.Name} Set Emby URL to {urlString}");
            }
        }

        [Command("embytoken")]
        [Summary("Sets the API Token For Emby")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Remarks("set embyurl w6e5f46s8df79we5f6dfs")]
        public async Task SetEmbyTokenAsync([Summary("The API Token")] string apiToken)
        {
            var contextGuild = Context.Guild;
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            Guild guildObject = null;
            foreach (var guild in obj.Guilds)
            {
                if (guild.Id == contextGuild.Id)
                {
                    guildObject = guild;
                }
            }
            if (guildObject != null)
            {
                EmbyData embyData;
                if (guildObject.EmbyData != null)
                {
                    embyData = guildObject.EmbyData;
                    embyData.EmbyToken = apiToken;
                }
                else
                {
                    embyData = new EmbyData
                    {
                        EmbyToken = apiToken
                    };
                }

                guildObject.EmbyData = embyData;

                rootObject.SerializeData<Guild>(obj);

                await Context.Channel.SendMessageAsync($"Set Emby API Token to {apiToken}");
                Console.WriteLine($"{DateTime.Now}");
                Console.WriteLine($"\t{contextGuild.Name} Set Emby API Token to {apiToken}");
            }
        }

        [Command("embyusername")]
        [Summary("Sets the Username to login into Emby")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Remarks("set embyusername username")]
        public async Task SetEmbyUsernameAsync([Summary("The username")] string username)
        {
            var contextGuild = Context.Guild;
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            Guild guildObject = null;
            foreach (var guild in obj.Guilds)
            {
                if (guild.Id == contextGuild.Id)
                {
                    guildObject = guild;
                }
            }
            if (guildObject != null)
            {
                EmbyData embyData;
                if (guildObject.EmbyData != null)
                {
                    embyData = guildObject.EmbyData;
                    embyData.Username = username;
                }
                else
                {
                    embyData = new EmbyData
                    {
                        Username = username
                    };
                }

                guildObject.EmbyData = embyData;

                rootObject.SerializeData<Guild>(obj);

                await Context.Channel.SendMessageAsync($"Set Emby Username to {username}");
                Console.WriteLine($"{DateTime.Now}");
                Console.WriteLine($"\t{contextGuild.Name} Set Emby Username to {username}");
            }
        }

        [Command("embypassword")]
        [Summary("Sets the Password to login into Emby")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Remarks("set embypassword password")]
        public async Task SetEmbyPasswordAsync([Summary("The password")] string password)
        {
            var contextGuild = Context.Guild;
            RootObject rootObject = new RootObject();
            var obj = rootObject.BaseClass();
            Guild guildObject = null;
            foreach (var guild in obj.Guilds)
            {
                if (guild.Id == contextGuild.Id)
                {
                    guildObject = guild;
                }
            }
            if (guildObject != null)
            {
                EmbyData embyData;
                if (guildObject.EmbyData != null)
                {
                    embyData = guildObject.EmbyData;
                    embyData.Password = password;
                }
                else
                {
                    embyData = new EmbyData
                    {
                        Password = password
                    };
                }

                guildObject.EmbyData = embyData;

                rootObject.SerializeData<Guild>(obj);

                await Context.Message.DeleteAsync();

                await Context.Channel.SendMessageAsync($"Set Emby Password");
                Console.WriteLine($"{DateTime.Now}");
                Console.WriteLine($"\t{contextGuild.Name} Set Emby password to {password}");
            }
        }
    }
}
