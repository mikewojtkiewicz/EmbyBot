using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.Data.Emby
{
    public class PublicUser
    {
        public string Name { get; set; }
        public Guid ServerId { get; set; }
        public Guid Id { get; set; }
        public bool HasPassword { get; set; }
        public bool HasConfiguredPassword { get; set; }
        public bool HasConfiguredEasyPassword { get; set; }
        public Configuration Configuration { get; set; }
        public Policy Policy { get; set; }
    }
}
