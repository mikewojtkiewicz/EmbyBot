namespace EmbyBot.Data.Emby
{
    public class PlayState
    {
        public bool CanSeek { get; set; }
        public bool IsPaused { get; set; }
        public bool IsMuted { get; set; }
        public string RepeatMode { get; set; }
        public int SubtitleOffset { get; set; }
        public int PlaybackRate { get; set; }
    }
}