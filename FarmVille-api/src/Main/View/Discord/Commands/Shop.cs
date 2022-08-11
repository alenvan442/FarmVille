using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.View.Discord.Commands;

namespace FarmVille.Commands
{
    ///
    /// <summary>
    /// This will hold all the neccessary commands that corresponds to player's interactions with the shop
    /// </summary>
    public class Shop: BaseCommandModule
    {

        /// <summary>
        /// the command to display the shop's window
        /// separated based on pages
        /// </summary>
        /// <param name="ctx"> the context of the command </param>
        /// <param name="pageNumber"> The page to display </param>
        /// <returns></returns>
        [Command("shop")]
        [Description("Displays the shop: .shop (page number)")]
        public async Task shop(CommandContext ctx, 
                                [Description("The number of the page to display, defaults to 1")] int pageNumber = 1 ) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            Tuple<int, String> pageItems = CommandsHelper.shopController.shopPage(currPlayer, pageNumber);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = "Shop",
                Description = pageItems.Item2

            }.WithFooter("page " + pageItems.Item1);

            await ctx.Channel.SendMessageAsync(embed);

        }

        /// <summary>
        /// The command that allows the user to purchase an item
        /// </summary>
        /// <param name="ctx"> the context of the command </param>
        /// <param name="item"> the name of the item to purchase </param>
        /// <param name="amount"> the amount to purchase </param>
        /// <returns></returns>
        [Command("buy")]
        [Description("Purchases an item from the shop: .buy (item name) (amount)")]
        public async Task buy(CommandContext ctx,
                        [Description("The name of the item to purchase")] string item,
                        [Description("The amount to buy, defaults to 1")] int amount = 1) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            item = item[0].ToString().ToUpper() + item.Substring(1);
            int result = CommandsHelper.shopController.buy(currPlayer, item, amount);

            if(item.Equals("Plantpot")) {
                amount = 1;
            }

            String message = "";
            switch(result) {
                case 0:
                    message = "Successfully purchased " + amount + " " + item + "(s)";
                    break;
                case 1:
                    message = "Insufficient funds to purchase " + amount + " " + item + "(s)";
                    break;
                case 2:
                    message = "Unable to find the item: " + item;
                    break;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Description = message,
                Color = DiscordColor.Aquamarine
            };

            await ctx.Channel.SendMessageAsync(embed);

        } 

        /// <summary>
        /// The command to allow a player to sell an item from their
        /// inventory
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="item"> The name of the item to sell </param>
        /// <param name="amount"> How many of the item to sell </param>
        /// <returns></returns>
        [Command("sell")]
        [Description("Sells an item: .sell (item name) (amount)")]
        public async Task sell(CommandContext ctx, 
                        [Description("The name of the item to sell")] string item,
                        [Description("The amount to sell, defaults to 1")] int amount = 1) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            item = item[0].ToString().ToUpper() + item.Substring(1);
            Item soldItem = CommandsHelper.shopController.sell(currPlayer, item, amount);

            String message = "";
            if(soldItem is null) {
                message = "Unable to find the item: " + item;
            } else {
                if(soldItem.amount == -1) {
                    message = "You do not have enough items to sell!";
                } else
                {
                    message = "Sold " + soldItem.amount + " " + soldItem.name + " for $" + soldItem.sellPrice * soldItem.amount;
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Description = message,
                Color = DiscordColor.Aquamarine
            };

            await ctx.Channel.SendMessageAsync(embed);

        }
        
    }
}