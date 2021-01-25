using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.Data
{
    public class Guild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string BotPrefix { get; set; }
        public EmbyData EmbyData { get; set; }
    }
}
