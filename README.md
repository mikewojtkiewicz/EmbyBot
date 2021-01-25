# EmbyBot
To use the Emby Bot with Discord, you need to make sure you have the following items setup:
1. A Public Emby Server
2. An API Key setup with your Emby Server
3. A Discord Bot Token

Once these are setup, build the Program in Visual Studio and add a config.json file to your debug folder for testing. Here is an example config.json file:
```
{
  "apiKey": "discord_api_key",
  "botFile": "C:\\ProgramData\\EmbyBot\\data.json"
}
```
The apiKey property is your Discord Bot Token and the botFile property is where you want the bot data to be stored.

After the config.json file has been setup, you can run the bot and invite it to your server. 

## Bot Commands
**login emby** - Logs in to your Emby server with the provided crendentials

**emby serveroverview** - Views the Overview of the Current Logged in Server

**emby play** - Plays the Video with the given ID

**emby search** - Searches the Emby database with the given search term

**emby browse** - Browses the entire Emby library

**help** - Get the list of commands for this bot

**set prefix** - Sets the bot prefix

**set embyurl** - Sets the URL For Emby

**set embytoken** - Sets the API Token For Emby

**set embyusername** - Sets the Username to login into Emby

**set embypassword** - Sets the Password to login into Emby
