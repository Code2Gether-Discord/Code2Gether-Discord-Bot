using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Code2Gether_Discord_Bot.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Code2Gether_Discord_Bot
{
    public interface IBot
    {
        Task RunAsync();
        Task RegisterCommandsAsync();
    }

    public class Bot : IBot
    {
        private ILogger _logger;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public Bot(ILogger logger)
        {
            _logger = logger;
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
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("token"));
            await _client.StartAsync();
        }

        #region Event Handlers
        /// <summary>
        /// Provide a way for the Discord API to log to our logger
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task LoggerHandler(LogMessage arg)
        {
            if (arg.Message.Length > 0)
                _logger.Log(arg.Severity, arg.Message);
        }

        /// <summary>
        /// When the bot first comes online
        /// </summary>
        /// <returns></returns>
        private async Task ReadyHandler()
        {

        }

        /// <summary>
        /// When the bot joins a guild
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task JoinedGuildHandler(SocketGuild arg)
        {

        }

        /// <summary>
        /// When a user joins the guild
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task UserJoinedHandler(SocketGuildUser arg)
        {

        }

        /// <summary>
        /// Check if new message is a command for this bot,
        /// and execute related command module
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task MessageReceivedHandler(SocketMessage arg)
        {
            // Parse incomming message
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
                _logger.Log(LogSeverity.Error, $"Failed to parse incomming message", e);
            }

            // Don't respond to bots or system msgs
            if (msg.Author.IsBot) return;
            if (msg.Author.IsWebhook) return;

            // If msg has this bot's prefix
            var prefix = Environment.GetEnvironmentVariable("prefix");
            int argPos = 0;
            if (!(msg.HasStringPrefix(prefix.ToLower(), ref argPos) || msg.HasStringPrefix(prefix.ToUpper(), ref argPos))) return;

            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (result.Error.HasValue) _logger.Log(LogSeverity.Debug, $"Message: {msg} returned: {result.Error.Value}");
            #endregion
        }
    }
}