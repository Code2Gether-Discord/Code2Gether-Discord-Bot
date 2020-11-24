# Code2Gether-Discord-Bot
A Discord bot for the Code2Gether server

# Commands
- c!get_excuse (get_excuse;getexcuse;) - Gets a random excuse.
- c!help (help;) - Returns this!
- c!info (info;about;whoami;owner;uptime;library;author;stats;) - Replies with a wealth of information regarding the bot's environment
- c!ping (ping;pong;) - Replies with an embed containing the bot's websocket latency
- c!makechannel (makechannel;) - Creates a new text channel

# How to add a new Command (as of commit e88286a)
- Say you want to add a command called `c!MyCommand`
- Use the existing command-related files for reference
1. Add a MyCommandModule.cs in Code2gether-Discord-Bot/Modules/UserModules/ (PrivilegedModules/ for role required commands)
2. Add a MyCommandLogic.cs in Code2gether-Discord-Bot.Library/BusinessLogic/
3. Wire Module class to Logic class via Code2gether-Discord-Bot/Static/BusinessLogicFactory.cs
