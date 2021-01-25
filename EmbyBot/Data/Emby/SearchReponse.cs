using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyBot.Data.Emby
{
    public class SearchReponse
    {
        public List<SearchItem> Items { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
