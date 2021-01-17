# Code2Gether-Discord-Bot
A Discord bot for the Code2Gether server

## How to add a new Command (as of commit e88286a)
- Say you want to add a command called `c!MyCommand`
- Use the existing command-related files for reference
1. Add a MyCommandModule.cs in Code2gether-Discord-Bot/Modules/UserModules/ (PrivilegedModules/ for role required commands)
2. Add a MyCommandLogic.cs in Code2gether-Discord-Bot.Library/BusinessLogic/
3. Wire Module class to Logic class via Code2gether-Discord-Bot/Static/BusinessLogicFactory.cs

## Help
c!getexcuse (getexcuse; excuse; ) - Gets a random excuse.
c!github list-org (github list-org; github lo; github list-organization; ) - List the GitHub Organization's Members.
c!github list-teams (github list-teams; github lt; ) - List the GitHub Organization's Teams.
c!github list-team-members (github list-team-members; github ltm; ) - List the GitHub Organization's Team Members. Usage: github-list-team-members [team-slug]
c!github join-org (github join-org; github jo; github join-organization; ) - Join the GitHub Organization at https://github.com/Code2Gether-Discord. Usage: github join-org [email
c!github join-team (github join-team; github jt; ) - Join the a GitHub Team in the Organization. Usage: github join-team [team-slug] [username]
c!help (help; ) - Returns this!
c!info (info; about; whoami; owner; uptime; library; author; stats; ) - Replies with a wealth of information regarding the bot's environment.
c!leet (leet; ) - Reply with a question from leetcode.com with a given title.
c!leetans (leetans; leetanswer; ) - Provide an answer for an open leet question.
c!ping (ping; pong; ) - Replies with an embed containing the bot's websocket latency.
c!makechannel (makechannel; ) - Creates a new text channel.
