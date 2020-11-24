using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Models;
using Code2Gether_Discord_Bot.Static;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Static;


namespace Code2Gether_Discord_Bot
{
    public class Bot : IBot
    {
        private ILogger _logger;
        private IConfig _config;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public Bot(ILogger logger, IConfig config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Register bot command modules
        /// </summary>
        /// <returns></returns>
        public async Task RegisterCommandsAsync()
        {
            try
            {
                var result = await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
                ModuleDetailRepository.Modules = result;
                if (result.Count() == 0) throw new Exception("No commands were detected");
                // Some nice logging
                result.ToList().ForEach(x =>
                {
                    string o = $"Loaded module: {x.Name} with commands: ";
                    x.Commands.ToList().ForEach(c =>
                    {
                        o += $"{c.Name}; ";
                    });
                    _logger.Log(LogSeverity.Info, o);
                });
            }
            catch (Exception e)
            {
                _logger.Log(LogSeverity.Critical, $"Failed to register commands! Exception: {e.Message}");
            }
        }

        /// <summary>
        /// Main bot process
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                // Additional config goes here
            });
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            await RegisterCommandsAsync();

            _client.Log += LoggerHandler;
            _client.Ready += ReadyHandler;
            _client.JoinedGuild += JoinedGuildHandler;
            _client.UserJoined += UserJoinedHandler;
            _client.MessageReceived += MessageReceivedHandler;

            await InitialStartupJobs();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Establishes connection to Discord
        /// </summary>
        /// <returns></returns>
        private async Task InitialStartupJobs()
        {
            await _client.LoginAsync(TokenType.Bot, _config.DiscordToken);
            await _client.StartAsync();
        }

        #region Event Handlers
        /// <summary>
        /// Provide a way for the Discord API to log to our logger
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task LoggerHandler(LogMessage arg)
        {
            if (arg.Message != null)
            {
                _logger.Log(arg.Severity, arg.Message);
            }
            else if (arg.Exception != null)
            {
                _logger.Log(arg.Severity, "Discord Log Event", arg.Exception);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// When the bot first comes online
        /// Debug: Won't set status
        /// </summary>
        /// <returns></returns>
        private Task ReadyHandler()
        {
            if (!_config.Debug)
            {
                // Set bot status
                string status = $"{_config.Prefix}help";
                return _client.SetGameAsync(status);
            }
            else return Task.CompletedTask;
        }

        /// <summary>
        /// When the bot joins a guild
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task JoinedGuildHandler(SocketGuild arg)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// When a user joins the guild
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task UserJoinedHandler(SocketGuildUser arg)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Check if new message is a command for this bot,
        /// and execute related command module
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task MessageReceivedHandler(SocketMessage arg)
        {
            // Parse incoming message
            SocketUserMessage msg = null;
            SocketCommandContext context = null;
            try
            {
                msg = arg as SocketUserMessage;
                if (msg != null)
                    context = new SocketCommandContext(_client, msg);
                else return;
            }
            catch (Exception e)
            {
                _logger.Log(LogSeverity.Error, $"Failed to parse incoming message", e);
            }

            // Don't respond to bots or system messages
            if (msg.Author.IsBot) return;
            if (msg.Author.IsWebhook) return;

            // If msg has this bot's prefix
            int argPos = 0;
            if (!(msg.HasMentionPrefix(_client.CurrentUser, ref argPos)    // If mention the bot in msg
                || msg.HasStringPrefix(_config.Prefix.ToLower(), ref argPos)    // Or lowercase prefix
                || msg.HasStringPrefix(_config.Prefix.ToUpper(), ref argPos)))  // Or uppercase prefix
                return; // Ignore msg if none of the previous conditions are met
            
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            
            if (result.Error.HasValue)
                _logger.Log(LogSeverity.Debug, $"Message: {msg} returned: {result.Error.Value}");
            #endregion
        }
    }
}