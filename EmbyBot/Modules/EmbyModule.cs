using Discord;
using Discord.Commands;
using EmbyBot.Data;
using EmbyBot.Data.Emby;
using EmbyBot.inc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmbyBot.Modules
{
    [Group("emby")]
    public class EmbyModule : ModuleBase<SocketCommandContext>
    {
        [Command("serveroverview")]
        [Summary("Views the Overview of the Current Logged in Server")]
        [Remarks("emby serveroverview")]
        public async Task ServerOverviewAsync()
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

                    if (embyUrl == null || embyToken == null)
                    {
                        await Context.Channel.SendMessageAsync($"You set both the Emby URL and Emby API token in order to login into Emby");
                    }
                    else
                    {
                        var systemJson = EmbyBase.EmbyGetWithToken(embyUrl, "System/Info", embyToken);
                        EmbySystemInfo systemInfo = JsonConvert.DeserializeObject<EmbySystemInfo>(systemJson);

                        var itemsJson = EmbyBase.EmbyGetWithToken(embyUrl, "Items/Counts", embyToken);
                        ItemCounts embyItems = JsonConvert.DeserializeObject<ItemCounts>(itemsJson);


                        EmbedAuthorBuilder author = new EmbedAuthorBuilder
                        {
                            Name = Context.Client.CurrentUser.Username
                        };

                        EmbedBuilder embedBuilder = new EmbedBuilder
                        {
                            Title = $"{systemInfo.ServerName}",
                            Description = $"Server Overview for {systemInfo.ServerName}",
                            Author = author
                        };

                        embedBuilder.AddField($"Movie Count", $"{embyItems.MovieCount}", true);
                        embedBuilder.AddField($"Series Count", $"{embyItems.SeriesCount}", true);
                        embedBuilder.AddField($"Episode Count", $"{embyItems.EpisodeCount}", true);

                        await ReplyAsync(string.Empty, false, embedBuilder.Build());
                    }
                }
            }
        }

        [Command("play")]
        [Summary("Plays the Video with the given ID")]
        [Remarks("emby play 6")]
        public async Task PlayMediaAsync([Summary("The Name of the Media item to play")] string title)
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

                    if (embyUrl == null || embyToken == null)
                    {
                        await Context.Channel.SendMessageAsync($"You need to set both the Emby URL and Emby API token in order to login into Emby");
                    }
                    else
                    {
                        var searchJson = EmbyBase.EmbyGetWithToken(embyUrl, $"Items", embyToken, $"&searchTerm={title}&Recursive=true&IncludeItemTypes=Movie");
                        SearchReponse searchResponse = JsonConvert.DeserializeObject<SearchReponse>(searchJson);
                        var mediaId = searchResponse.Items[0].Id;
                        if (searchResponse.Items.Count == 1)
                        {
                            var mediaJson = EmbyBase.EmbyGetWithToken(embyUrl, $"Users/{guildObject.EmbyData.UserData.UserId}/Items/{mediaId}", embyToken, "&Recursive=true&IncludeItemTypes=Movie");
                            MediaItem mediaItem = JsonConvert.DeserializeObject<MediaItem>(mediaJson);


                            EmbedAuthorBuilder author = new EmbedAuthorBuilder
                            {
                                Name = Context.Client.CurrentUser.Username
                            };

                            string genreBuilder = string.Empty;
                            int genreCount = mediaItem.Genres.Length;
                            foreach (var genre in mediaItem.Genres)
                            {
                                genreCount -= 1;
                                if (genreCount == 0)
                                    genreBuilder += $"{genre}";
                                else
                                    genreBuilder += $"{genre}, ";
                            }

                            EmbedBuilder embedBuilder = new EmbedBuilder
                            {
                                Title = $"{mediaItem.Name} - {mediaItem.OfficialRating}",
                                Description = $"{genreBuilder}",
                                ThumbnailUrl = $"{embyUrl}/emby/Items/{mediaId}/Images/Primary"
                            };

                            embedBuilder.AddField($"Overview", $"{mediaItem.Overview}", false);
                            embedBuilder.AddField($"Play", $"[Play Trailer]({mediaItem.RemoteTrailers[0].Url})\n[Play Movie]({embyUrl}/emby/Videos/{mediaId}/stream?DeviceId=embybot&Container={mediaItem.Container}&api_key={embyToken})", true);

                            string externalUrls = string.Empty;
                            foreach(var url in mediaItem.ExternalUrls)
                            {
                                externalUrls += $"[{url.Name}]({url.Url})\n";
                            }
                            embedBuilder.AddField($"Links", $"{externalUrls}", true);
                            //embedBuilder.AddField($"Episode Count", $"{embyItems.EpisodeCount}", true);

                            await ReplyAsync(string.Empty, false, embedBuilder.Build());
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("Your entry yielded too many results. Please enter the exact name of the media item you wish to play.");
                        }
                    }
                }
            }
        }

        [Command("search")]
        [Summary("Searches the Emby database with the given search term")]
        [Remarks("emby search \"star wars\"")]
        public async Task EmbySearchAsync([Summary("The term to search")] string searchTerm)
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

                    if (embyUrl == null || embyToken == null)
                    {
                        await Context.Channel.SendMessageAsync($"You set both the Emby URL and Emby API token in order to login into Emby");
                    }
                    else
                    {
                        if(guildObject.EmbyData.UserData != null)
                        {
                            var searchJson = EmbyBase.EmbyGetWithToken(embyUrl, $"Items", embyToken, $"&searchTerm={searchTerm}&Recursive=true&IncludeItemTypes=Movie");
                            SearchReponse searchResponse = JsonConvert.DeserializeObject<SearchReponse>(searchJson);

                            try
                            {
                                string stringBuilder = "```";
                                stringBuilder += $"Search Query:   {searchTerm}\n";
                                stringBuilder += $"Search Results: {searchResponse.TotalRecordCount}\n\n";
                                stringBuilder += "          Name           |   MPAA Rating  |         Runtime         |   Community Rating  |                   Trailer                     |\n";
                                stringBuilder += "-------------------------|----------------|-------------------------|---------------------|-----------------------------------------------|\n";

                                foreach (var item in searchResponse.Items)
                                {
                                    var itemJson = EmbyBase.EmbyGetWithToken(embyUrl, $"emby/Users/{guildObject.EmbyData.UserData.UserId}/Items/{item.Id}", embyToken);
                                    MediaItem mediaItem = JsonConvert.DeserializeObject<MediaItem>(itemJson);

                                    if(mediaItem.Container == "mkv")
                                    {
                                        continue;
                                    }

                                    var newEntryLine = string.Empty;

                                    var itemTitle = string.Empty;
                                    var titleLength = 0;
                                    var nameLength = item.Name.Length;
                                    if(nameLength <= 24)
                                    {
                                        itemTitle = item.Name;
                                        titleLength = nameLength;
                                    }
                                    else
                                    {
                                        itemTitle = item.Name.Substring(0, 24);
                                        titleLength = itemTitle.Length;
                                    }


                                    var mpaaLength = mediaItem.OfficialRating.Length;
                                    var communityRatingLength = mediaItem.CommunityRating.ToString().Length;
                                    
                                    var trailerUrlLength = 0;
                                    var trailerUrl = string.Empty;
                                    if (mediaItem.RemoteTrailers.Count > 0)
                                    {
                                        trailerUrl = mediaItem.RemoteTrailers[0].Url;
                                        trailerUrlLength = mediaItem.RemoteTrailers[0].Url.Length;
                                    }                                    

                                    var runtimeMilliseconds = item.RunTimeTicks / 10000;
                                    var runtimeMinutes = runtimeMilliseconds / 60000;

                                    var totalHours = 0;
                                    var totalMinutes = 0;
                                    while (runtimeMinutes > 60)
                                    {
                                        totalHours += 1;
                                        runtimeMinutes -= 60;
                                    }

                                    totalMinutes = (int)runtimeMinutes;
                                    string runtime = $"{totalHours}h {totalMinutes}m";

                                    newEntryLine += $" {itemTitle}{new String(' ', (24 - titleLength))}| " +
                                        $"{mediaItem.OfficialRating}{new String(' ', 15 - mpaaLength)}| " +
                                        $"{runtime}{new String(' ', 24 - runtime.Length)}| " +
                                        $"{mediaItem.CommunityRating}{new String(' ', 20 - communityRatingLength)}| " +
                                        $"{trailerUrl}{new String(' ', 46 - trailerUrlLength)}|\n";

                                    stringBuilder += $"{newEntryLine}";
                                }

                                stringBuilder += "```";

                                await Context.Channel.SendMessageAsync(stringBuilder);
                            }
                            catch (Exception e)
                            {
                                await Context.Channel.SendMessageAsync(e.Message);
                                await Context.Channel.SendMessageAsync(e.StackTrace);
                            }
                        }
                    }
                }
            }
        }

        [Command("browse")]
        [Summary("Browses the entire Emby library")]
        [Remarks("emby browse")]
        public async Task EmbyBrowseAsync()
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

                    if (embyUrl == null || embyToken == null)
                    {
                        await Context.Channel.SendMessageAsync($"You set both the Emby URL and Emby API token in order to login into Emby");
                    }
                    else
                    {
                        string stringBuilder = EmbyBrowse.BrowseLibrary(embyUrl, embyToken, guildObject);
                        var contentMessage = await Context.Channel.SendMessageAsync(stringBuilder);
                        await contentMessage.AddReactionAsync(new Emoji("⬅️"));
                        await contentMessage.AddReactionAsync(new Emoji("➡️"));
                    }
                }
            }
        }
    }
}
