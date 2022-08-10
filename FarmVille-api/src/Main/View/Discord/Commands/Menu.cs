using System.Linq.Expressions;
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
using FarmVille_api.src.Main.View.Discord.Commands;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will hold all basic menu commands such as help, shop, inventory etc.
    /// </summary>
    public class Menu: BaseCommandModule
    {

        /// <summary>
        /// Will display the help menu 
        /// This will display various categories and commands with their functionality
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("helpMenu")]
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

            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.Member.Id);

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
        /// Displays a list of plant pots that the player has
        /// separated based on pages
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="pageIndex"> The page to display </param>
        /// <returns></returns>
        [Command("pots")]
        public async Task displayPots(CommandContext ctx, int pageIndex = 1) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            List<String> pots = currPlayer.getPots(pageIndex);

            String pageString = "\n";
            foreach (String i in pots)
            {
                pageString += i + "\n\n";
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Turquoise,
                Title = ctx.User.Username + "'s Pots",
                Description = pageString
            }.WithFooter("page " + pageIndex);

            await ctx.Channel.SendMessageAsync(embed);

        }

        /// <summary>
        /// Displays a player's inventory based on pages
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <param name="pageIndex"> The page to display </param>
        /// <returns></returns>
        [Command("bag")]
        public async Task displayPlayerInventory(CommandContext ctx, int pageIndex = 1) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            List<String> inventory = currPlayer.getInventory(pageIndex);

            String pageString = "";
            foreach (String i in inventory)
            {
                pageString += "\n" + i;
            }

            pageString += "\n";

            //create the embed
            DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = ctx.User.Username + "'s Inventory",
                Description = pageString

            }.WithFooter("page " + pageIndex);

            await ctx.Channel.SendMessageAsync(baseEmbed);

        }
        
    }
}