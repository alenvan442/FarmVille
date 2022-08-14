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
        /// Displays the player's status
        /// Ex: Name, Id, and current money holdings
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <returns></returns>
        [Command("status")]
        [Description("Displays the player's status: .status")]
        public async Task displayPlayerStatus(CommandContext ctx) {

            //create the embed
            DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = ctx.User.Username + "'s Status Card",
                Description = CommandsHelper.playerController.getStatus(ctx.User.Id)

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
        [Description("Displays the player's plant pots: .pots (page number)")]
        public async Task displayPots(CommandContext ctx,
                        [Description("The number of the page to display, defailts to 1")] int pageIndex = 1) {
            Tuple<int, List<String>> potList = CommandsHelper.playerController.getPots(ctx.User.Id, pageIndex);

            String pageString = "\n";
            foreach (String i in potList.Item2)
            {
                pageString += i + "\n\n";
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Turquoise,
                Title = ctx.User.Username + "'s Pots",
                Description = pageString
            }.WithFooter("page " + potList.Item1);

            await ctx.Channel.SendMessageAsync(embed);

        }

        /// <summary>
        /// Displays a player's inventory based on pages
        /// </summary>
        /// <param name="ctx"> The Context of the command </param>
        /// <param name="pageIndex"> The page to display </param>
        /// <returns></returns>
        [Command("bag")]
        [Description("Displays the player's inventory: .bag (page number)")]
        public async Task displayPlayerInventory(CommandContext ctx,
                        [Description("The number of the page to display, defaults to 1")] int pageIndex = 1) {
            Tuple<int, List<String>> inventory = CommandsHelper.playerController.getInventory(ctx.User.Id, pageIndex);

            String pageString = "";
            foreach (String i in inventory.Item2)
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

            }.WithFooter("page " + inventory.Item1);

            await ctx.Channel.SendMessageAsync(baseEmbed);

        }
        
    }
}