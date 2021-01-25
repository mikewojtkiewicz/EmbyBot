using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.Data.Emby
{
    public class LoginResponse
    {
        public PublicUser User { get; set; }
        public SessionInfo SessionInfo { get; set; }
        public Guid AccessToken { get; set; }
        public Guid ServerId { get; set; }
    }
}
