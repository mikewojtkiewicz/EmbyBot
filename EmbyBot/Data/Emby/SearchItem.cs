namespace EmbyBot.Data.Emby
{
    public class SearchItem
    {
        public string Name { get; set; }
        public string ServerId { get; set; }
        public long Id { get; set; }
        public long RunTimeTicks { get; set; }
        public bool IsFolder { get; set; }
        public string Type { get; set; }
        public ImageTags ImageTags { get; set; }
        public string[] BackdropImageTags { get; set; }
        public string MediaType { get; set; }
    }
}