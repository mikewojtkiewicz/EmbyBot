using Discord;
using Discord.WebSocket;
using EmbyBot.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace EmbyBot
{
    class Program
    {
        private static DiscordSocketClient _client;
        private IServiceProvider _services;
        private HandlerHandler _handler;


        public async Task StartAsync()
        {
            Version build = Assembly.GetExecutingAssembly().GetName().Version;
            string buildNumber = $"{build.Major}.{build.Minor}.{build.Build}.{build.Revision}";


            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Error
            });
            _client.Log += Log;


            Task t = Task.Run(async () =>
            {
                var init = new Initialize(null, _client);
                _services = init.BuildServiceProvider();

                Console.WriteLine($"Connecting to Emby Bot...");

                await _client.LoginAsync(TokenType.Bot, BotToken());
                await _client.StartAsync();


                _handler = new HandlerHandler(_client, _services);
            });

            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(_client.ConnectionState);
            Console.WriteLine("------------------------LOGIN-DETAILS------------------------");
            Console.WriteLine($"Logged in as {_client.CurrentUser.Username}");
            Console.WriteLine($"Build {buildNumber}");
            Console.WriteLine($"Connected to {_client.Guilds.Count} guilds");
            Console.WriteLine($"Invite at: https://discordapp.com/oauth2/authorize?client_id={_client.CurrentUser.Id}&permissions=8&scope=bot");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(string.Empty);


            await Task.Delay(-1);
        }

        static void Main() => new Program().StartAsync().GetAwaiter().GetResult();

        private string BotToken()
        {
            using StreamReader file = File.OpenText("config.json");
            using JsonTextReader reader = new JsonTextReader(file);
            var val = reader.Value;
            JObject o2 = (JObject)JToken.ReadFrom(reader);
            return o2["apiKey"].ToString();
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
