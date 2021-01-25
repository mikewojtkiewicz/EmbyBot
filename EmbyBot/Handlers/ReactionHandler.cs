using Discord;
using Discord.WebSocket;
using EmbyBot.Data;
using EmbyBot.inc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmbyBot.Handlers
{
    public class ReactionHandler
    {
        private DiscordSocketClient _client;
        public ReactionHandler(DiscordSocketClient client, IServiceProvider services)
        {
            _client = client;

            _client.ReactionAdded += ReactionAdded;
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> messageCache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var message = await messageCache.GetOrDownloadAsync();
            if (!reaction.User.Value.IsBot)
            {
                var channelData = (SocketTextChannel)channel;

                RootObject rootObject = new RootObject();
                var obj = rootObject.BaseClass();
                Guild guildObject = null;
                foreach (var guild in obj.Guilds)
                {
                    if (guild.Id == channelData.Guild.Id)
                    {
                        guildObject = guild;
                    }
                }
                
                if (reaction.Emote.Name == "⬅️")
                {
                    if (guildObject != null)
                    {
                        if (guildObject.EmbyData != null)
                        {
                            string embyUrl = guildObject.EmbyData.EmbyUrl;
                            string embyToken = guildObject.EmbyData.EmbyToken;

                            if (embyUrl == null || embyToken == null)
                            {

                            }
                            else
                            {
                                var messageContent = message.Content;
                                int pageNumberIndex = messageContent.IndexOf("`", StringComparison.Ordinal);
                                string pageNumberText = messageContent.Substring(0, pageNumberIndex);

                                int pageNumber = 0;
                                string splitPattern = @"[^\d]";
                                string[] result = Regex.Split(pageNumberText, splitPattern);
                                pageNumber = Convert.ToInt32(string.Join("", result.Where(e => !String.IsNullOrEmpty(e))));

                                if (pageNumber > 1)
                                {
                                    int pageIndex = (pageNumber * 10) - 20;
                                    EmbyBrowse.BrowseLibrary(embyUrl, embyToken, guildObject, pageIndex);

                                    string stringBuilder = EmbyBrowse.BrowseLibrary(embyUrl, embyToken, guildObject, pageIndex);

                                    await message.ModifyAsync(m => m.Content = stringBuilder, null);
                                    await message.RemoveReactionAsync(reaction.Emote, reaction.User.Value);
                                }
                            }
                        }
                    }
                }
                else if (reaction.Emote.Name == "➡️")
                {
                    if (guildObject != null)
                    {
                        if (guildObject.EmbyData != null)
                        {
                            string embyUrl = guildObject.EmbyData.EmbyUrl;
                            string embyToken = guildObject.EmbyData.EmbyToken;

                            if (embyUrl == null || embyToken == null)
                            {

                            }
                            else
                            {
                                var messageContent = message.Content;
                                int pageNumberIndex = messageContent.IndexOf("`", StringComparison.Ordinal);
                                string pageNumberText = messageContent.Substring(0, pageNumberIndex);

                                int pageNumber = 0;
                                string splitPattern = @"[^\d]";
                                string[] result = Regex.Split(pageNumberText, splitPattern);
                                pageNumber = Convert.ToInt32(string.Join("", result.Where(e => !String.IsNullOrEmpty(e))));

                                int pageIndex = ((pageNumber + 1) * 10) - 10;
                                string stringBuilder = EmbyBrowse.BrowseLibrary(embyUrl, embyToken, guildObject, pageIndex);

                                await message.ModifyAsync(m => m.Content = stringBuilder, null);
                                await message.RemoveReactionAsync(reaction.Emote, reaction.User.Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
