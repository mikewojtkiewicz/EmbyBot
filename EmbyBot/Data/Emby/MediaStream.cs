namespace EmbyBot.Data.Emby
{
    public class MediaStream
    {
        public string Codec { get; set; }
        public string CodecTag { get; set; }
        public string Language { get; set; }
        public string TimeBase { get; set; }
        public string CodecTimeBase { get; set; }
        public string VideoRange { get; set; }
        public string DisplayTitle { get; set; }
        public int NalLengthSize { get; set; }
        public bool IsInterlaced { get; set; }
        public bool IsAVC { get; set; }
        public long BitRate { get; set; }
        public int BitDepth { get; set; }
        public int RefFrames { get; set; }
        public bool IsDefault { get; set; }
        public bool IsForced { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public float AverageFrameRate { get; set; }
        public float RealFrameRate { get; set; }
        public string Profile { get; set; }
        public string Type { get; set; }
        public string AspectRatio { get; set; }
        public int Index { get; set; }
        public bool IsExternal { get; set; }
        public bool IsTextSubtitleStream { get; set; }
        public bool SupportsExternalStream { get; set; }
        public string Protocol { get; set; }
        public string PixelFormat { get; set; }
        public int Level { get; set; }
        public bool IsAnamorphic { get; set; }
    }
}