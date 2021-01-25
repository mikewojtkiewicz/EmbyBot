using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.Data.Emby
{
    public class MediaItem
    {
        public string Name { get; set; }
        public string OriginalTitle { get; set; }
        public Guid ServerId { get; set; }
        public int Id { get; set; }
        public Guid Etag { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CanDelete { get; set; }
        public bool CanDownload { get; set; }
        public string PresentationUniqueKey { get; set; }
        public bool SupportsSync { get; set; }
        public string Container { get; set; }
        public string SortName { get; set; }
        public DateTime PremiereDate { get; set; }
        public List<ExternalUrl> ExternalUrls { get; set; }
        public List<MediaSource> MediaSources { get; set; }
        public int CriticRating { get; set; }
        public string[] ProductionLocations { get; set; }
        public string Path { get; set; }
        public string OfficialRating { get; set; }
        public string Overview { get; set; }
        public string[] Taglines { get; set; }
        public string[] Genres { get; set; }
        public float CommunityRating { get; set; }
        public long RunTimeTicks { get; set; }
        public string PlayAccess { get; set; }
        public int ProductionYear { get; set; }
        public List<RemoteTrailer> RemoteTrailers { get; set; }
        public ProviderIds ProviderIds { get; set; }
        public bool IsFolder { get; set; }
        public int ParentId { get; set; }
        public string Type { get; set; }
        public List<People> People { get; set; }
        public List<Studio> Studios { get; set; }
        public List<GenreItem> GenreItems { get; set; }
        public string[] TagItems { get; set; }
        public UserData UserData { get; set; }
        public string DisplayPreferencesId { get; set; }
        public string[] Tags { get; set; }
        public float PrimaryImageAspectRatio { get; set; }
        public List<MediaStream> MediaStreams { get; set; }
        public ImageTags ImageTags { get; set; }
        public string[] BackdropImageTags { get; set; }
        public List<Chapter> Chapters { get; set; }
        public string MediaType { get; set; }
        public string[] LockedFields { get; set; }
        public bool LockData { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
