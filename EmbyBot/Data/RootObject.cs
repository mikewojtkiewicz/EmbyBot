using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmbyBot.Data
{
    public class RootObject
    {
        public List<Guild> Guilds { get; set; }

        public RootObject BaseClass()
        {
            BotFile botFile = new BotFile();

            RootObject obj = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(botFile.GetBotFile()));

            return obj;
        }
        public object SerializeData<T>(object data)
        {
            BotFile botFile = new BotFile();
            JsonConvert.SerializeObject(data, Formatting.Indented);

            using (StreamWriter file = File.CreateText(botFile.GetBotFile()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }

            return null;
        }
    }
}
