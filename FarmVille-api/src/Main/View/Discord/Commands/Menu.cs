using System.Drawing;
using System.Net.Http;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model;
using FarmVille_api.src.Main.Model.Structures;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will hold all basic menu commands such as help, shop, inventory etc.
    /// </summary>
    public class Menu: BaseCommandModule
    {

        PlayerController playerController;
        public Menu(PlayerController playerController) {
            this.playerController = playerController;
        }

        /// <summary>
        /// Will display the help menu 
        /// This will display various categories and commands with their functionality
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("help")]
        public async Task helpMenu(CommandContext ctx) {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Will display a menu that holds some various commands or navigations
        /// Inventory, staus, help, achievements etc.
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("menu")]
        public async Task displayMenu(CommandContext ctx) {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Displays the player's status
        /// Ex: Name, Id, and current money holdings
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("status")]
        public async Task displayPlayerStatus(CommandContext ctx) {

            Player currPlayer = playerController.getPlayer(ctx.Member.Id);

            //create the embed
            DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = ctx.Member.Username + "'s Status Card",
                Description = currPlayer.ToString()

            };

            await ctx.Channel.SendMessageAsync(baseEmbed);

        }

        /// <summary>
        /// Displays a player's inventory
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("bag")]
        public async Task displayPlayerInventory(CommandContext ctx) {
            Player currPlayer = playerController.getPlayer(ctx.Member.Id);
            String[] inventory = currPlayer.getInventory();
            DiscordChannel outputChannel = ctx.Channel;

            DiscordClient client = ctx.Client;
            var interactivity = client.GetInteractivity();

            //create the embed
            DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = ctx.Member.Username + "'s Inventory"

            };

            String pageString = "";
            foreach(String i in inventory) {
                pageString += i + "\n";
            }

            var pages = interactivity.GeneratePagesInEmbed(pageString);

            //create the left and right emojis 
            PaginationEmojis buttons = new PaginationEmojis
            {
                Left = DiscordEmoji.FromName(client, ":arrow_left:"),
                Right = DiscordEmoji.FromName(client, ":arrow_right:"),
                SkipLeft = null,
                SkipRight = null,
                Stop = null

            };

            //send the pages
            await interactivity.SendPaginatedMessageAsync(ctx.Channel, ctx.User, pages, emojis: buttons,
                PaginationBehaviour.Ignore,
                PaginationDeletion.KeepEmojis);


        }


        
    }
}