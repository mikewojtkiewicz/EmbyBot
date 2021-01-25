using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmbyBot.Data
{
    public class BotFile
    {
        public string GetBotFile()
        {
            using StreamReader file = File.OpenText("config.json");
            using JsonTextReader reader = new JsonTextReader(file);
            JObject o2 = (JObject)JToken.ReadFrom(reader);
            return o2["botFile"].ToString();
        }
    }
}
