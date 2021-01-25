using System;

namespace EmbyBot.Data.Emby
{
    public class SessionInfo
    {
        public PlayState PlayState { get; set; }
        public string[] AdditionalUsers { get; set; }
        public string RemoteEndPoint { get; set; }
        public string[] PlayableMediaTypes { get; set; }
        public int PlaylistIndex { get; set; }
        public int PlaylistLength { get; set; }
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Client { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public string ApplicationVersion { get; set; }
        public string[] SupportedCommands { get; set; }
        public bool SupportsRemoteControl { get; set; }
    }
}