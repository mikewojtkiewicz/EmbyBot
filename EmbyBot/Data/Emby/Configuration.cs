namespace EmbyBot.Data.Emby
{
    public class Configuration
    {
        public bool PlayDefaultAudioTrack { get; set; }
        public bool DisplayMissingEpisodes { get; set; }
        public string[] GroupedFolders { get; set; }
        public string SubtitleMode { get; set; }
        public bool DisplayCollectionsView { get; set; }
        public bool EnableLocalPassword { get; set; }
        public string[] OrderedViews { get; set; }
        public string[] LatestItemsExcludes { get; set; }
        public string[] MyMediaExcludes { get; set; }
        public bool HidePlayedInLatest { get; set; }
        public bool RememberAudioSelections { get; set; }
        public bool RememberSubtitleSelections { get; set; }
        public bool EnableNextEpisodeAutoPlay { get; set; }
    }
}