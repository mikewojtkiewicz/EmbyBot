using System.Collections.Generic;

namespace EmbyBot.Data.Emby
{
    public class MediaSource
    {
        public string Protocol { get; set; }
        public string Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Container { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public bool IsRemote { get; set; }
        public long RunTimeTicks { get; set; }
        public bool SupportsTranscoding { get; set; }
        public bool SupportsDirectStream { get; set; }
        public bool SupportsDirectPlay { get; set; }
        public bool IsInfiniteStream { get; set; }
        public bool RequiresOpening { get; set; }
        public bool RequiresClosing { get; set; }
        public bool RequiresLooping { get; set; }
        public bool SupportsProbing { get; set; }
        public List<MediaStream> MediaStreams { get; set; }
        public string[] Formats { get; set; }
        public long Bitrate { get; set; }
        public RequiredHttpHeaders RequiredHttpHeaders { get; set; }
        public bool ReadAtNativeFramerate { get; set; }
        public int DefaultAudioStreamIndex { get; set; }
    }
}