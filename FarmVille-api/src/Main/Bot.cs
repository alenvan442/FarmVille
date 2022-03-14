using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;


/*
 * the main bot class
 * this file handles the initialization of the bot and what it should do
 * upon entering various states
 */
namespace FarmVille {
    public class Bot {
        //declare the client and commands extenstions
        public DiscordClient client { get; set; }
        public CommandsNextExtension commands { get; set; }

        //upon bot startup, do this
        public async Task RunAsync() {
            //create the configuration of the bot
            var clientConfig = new DiscordConfiguration {
                AutoReconnect = true,
                Intents = DiscordIntents.All,
                Token = StaticUtil.token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            //set the client's configs
            client = new DiscordClient(clientConfig);
            client.GuildAvailable += OnGuildAvailable;
            client.Ready += OnReady;

            //initialize the commands configurations
            var commandConfig = new CommandsNextConfiguration {
                CaseSensitive = false,
                EnableDefaultHelp = true,
                EnableDms = false,
                IgnoreExtraArguments = true,
                StringPrefixes = new string[] { "." },

            };

            //set the commands of the bot
            commands = client.UseCommandsNext(commandConfig);
            commands.RegisterCommands<Menu>();
            commands.RegisterCommands<Farming>();

            //Set up interactivity
            client.UseInteractivity(new InteractivityConfiguration() {
                Timeout = TimeSpan.FromMinutes(5)
            });

            //connect the bot to the client
            await client.ConnectAsync();
            await Task.Delay(-1);

        }

        /*
         * This function fires once the client is ready
         * 
         * @param client: the client that is ready
         * @param e: the args that are passed in once the client is ready
         */
        public async Task OnReady(DiscordClient client, ReadyEventArgs e) {
            await Database.InitializeJsonItems();
            await PlayerCommands.PlayerLoadAll();
        }

        /*
         * This function fires once the client joins a guild and recognizes the guild
         * 
         * @param client: the client that has joined a guild
         * @param e: the args that are passed in once the client connects to a guild
         */
        public async Task OnGuildAvailable(DiscordClient client, GuildCreateEventArgs e) {
            PlayerCommands.PlayerAddAll(e.Guild);
            await Task.CompletedTask;
        }

    }
}
