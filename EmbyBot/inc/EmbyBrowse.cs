using EmbyBot.Data;
using EmbyBot.Data.Emby;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.inc
{
    public class EmbyBrowse
    {
        public static string BrowseLibrary(string embyUrl, string embyToken, Guild guildObject, int startIndex = 0, int limit = 10)
        {
            int pageNumber = 1;
            if(startIndex != 0)
            {
                pageNumber = (startIndex / limit) + 1;
            }

            string stringBuilder = $"Page Number: {pageNumber}";
            stringBuilder += "```";
            //StartIndex=0&Limit=15
            var browseJson = EmbyBase.EmbyGetWithToken(embyUrl, $"emby/Items", embyToken, $"&StartIndex={startIndex}&Limit={limit}&Recursive=true&Fields=MediaSources&IncludeItemTypes=movie&SortBy=Sortname");
            SearchReponse browseResponse = JsonConvert.DeserializeObject<SearchReponse>(browseJson);

            try
            {                
                //stringBuilder += $"Search Query:   {searchTerm}\n";
                //stringBuilder += $"Search Results: {searchResponse.TotalRecordCount}\n\n";
                stringBuilder += "          Name           |    Released    |   MPAA Rating  |         Runtime         |   Community Rating  |\n";
                stringBuilder += "-------------------------|----------------|----------------|-------------------------|---------------------|\n";

                foreach (var item in browseResponse.Items)
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
                    if (nameLength <= 24)
                    {
                        itemTitle = item.Name;
                        titleLength = nameLength;
                    }
                    else
                    {
                        itemTitle = item.Name.Substring(0, 24);
                        titleLength = itemTitle.Length;
                    }


                    var mpaaLength = 0;
                    var mpaaText = string.Empty;
                    if(mediaItem.OfficialRating != null)
                    {
                        mpaaLength = mediaItem.OfficialRating.Length;
                        mpaaText = mediaItem.OfficialRating;
                    }
                    else
                    {
                        mpaaText = "NR";
                        mpaaLength = mpaaText.Length;
                    }

                    var communityRatingLength = mediaItem.CommunityRating.ToString().Length;

                    var releaseDate = mediaItem.PremiereDate.ToString("MM/dd/yyyy");
                    var releaseDateLength = releaseDate.Length;

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
                        $"{releaseDate}{new String(' ', 15 - releaseDateLength)}| " +
                        $"{mpaaText}{new String(' ', 15 - mpaaLength)}| " +
                        $"{runtime}{new String(' ', 24 - runtime.Length)}| " +
                        $"{mediaItem.CommunityRating}{new String(' ', 20 - communityRatingLength)}| \n";

                    stringBuilder += $"{newEntryLine}";
                }

                stringBuilder += "```";

                return stringBuilder;
                
            }
            catch (Exception e)
            {
                return e.Message + "\n" + e.StackTrace;
            }
        }
    }
}
