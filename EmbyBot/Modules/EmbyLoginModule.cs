using Discord;
using Discord.Commands;
using Discord.Addons.Interactive;
using EmbyBot.Data;
using EmbyBot.Data.Emby;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Modules
{
    [Group("login")]
    [RequireUserPermission(GuildPermission.Administrator, Group = "Administrator")]
    [RequireOwner(Group = "Administrator")]
    public class EmbyLoginModule : ModuleBase<SocketCommandContext>
    {
        [Command("emby")]
        public async Task TestEmbyAsync()
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
                if (guildObject.EmbyData != null)
                {
                    string embyUrl = guildObject.EmbyData.EmbyUrl;
                    string embyToken = guildObject.EmbyData.EmbyToken;

                    if(embyUrl == null || embyToken == null)
                    {
                        await Context.Channel.SendMessageAsync($"You set both the Emby URL and Emby API token in order to login into Emby");
                    }
                    else
                    {
                        var json = EmbyBase.EmbyGetWithToken(embyUrl, "Users/Public", embyToken);

                        List<PublicUser> publicUsers = JsonConvert.DeserializeObject<List<PublicUser>>(json);
                        if(publicUsers.Count > 0)
                        {
                            EmbyLogin login = new EmbyLogin();

                            foreach(var user in publicUsers)
                            {
                                if(user.Name == guildObject.EmbyData.Username)
                                {
                                    login.Username = guildObject.EmbyData.Username;
                                    login.Pw = guildObject.EmbyData.Password;
                                }
                            }

                            if(login.Username != null)
                            {
                                var loginJson = EmbyBase.EmbyLogin(embyUrl, embyToken, publicUsers[0].Id.ToString(), login);
                                LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(loginJson);

                                EmbyUser newEmbyUser = new EmbyUser
                                {
                                    AccessToken = response.AccessToken,
                                    UserId = response.User.Id
                                };
                                guildObject.EmbyData.UserData = newEmbyUser;
                                rootObject.SerializeData<Guild>(obj);

                                await Context.Channel.SendMessageAsync($"{guildObject.EmbyData.Username} logged in successfully!");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync("The username or password you set in the Bot config does not match a Public User. Please check the username and password and try again.");
                            }
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("You do not have any public users. Please make a public user in Emby and try again");
                        }                    
                    }
                }
            }
        }
    }
}
