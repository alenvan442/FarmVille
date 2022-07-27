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
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will hold all basic menu commands such as help, shop, inventory etc.
    /// </summary>
    public class Menu: BaseCommandModule
    {

        PlayerController playerController;
        EmbedUtilities embedUtilities;

        public Menu(PlayerController playerController, EmbedUtilities embedUtilities) {
            this.playerController = playerController;
            this.embedUtilities = embedUtilities;
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
            Player currPlayer = playerController.getPlayer(ctx.User.Id);
            String[] inventory = currPlayer.getInventory();

            //create the embed
            DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = ctx.User.Username + "'s Inventory"

            };

            String pageString = "";
            foreach(String i in inventory) {
                pageString += i + "\n";
            }

            await this.embedUtilities.sendPagination(ctx.Channel, pageString, ctx.User, ctx.Client);

        }


        
    }
}