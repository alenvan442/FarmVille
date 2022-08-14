using System.Drawing;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Utilities;
using FarmVille_api.src.Main.View.Discord.Commands;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will consist of all commands that contribute to farming in the game, typically this will look towards 
    /// specifically the plant pots that the players hold
    /// </summary>
    public class Farming: BaseCommandModule
    {

        

        /// <summary>
        /// The plant command, used for a player to plant a seed into a plant pot
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="input"> Any input the player makes, whether it is a seed name or seed id </param>
        /// <returns>  </returns>
        [Command("plant")]
        [Description("Plants a seed in an empty plant pot: .plant (seed name)")]
        public async Task plant(CommandContext ctx,
                        [Description("The name of the seed to plant")] string input = "") {

            if (input?.Length <= 0 || input == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a seed to plant and use the command: /plant (seed name)\n" +
                                                    "Or use the command: /seeds (page number), to view your list of seeds");
            } else {
                input = input[0].ToString().ToUpper() + input.Substring(1);
                String result = CommandsHelper.plantPotController.plantSeed(ctx.User.Id, input);

                await ctx.Channel.SendMessageAsync(result);
            }
        }

        /// <summary>
        /// Displays an embed listing the seeds that the player has in their inventory
        /// seperated by pages
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="pageIndex"> The page to display </param>
        /// <returns></returns>
        [Command("seeds")]
        [Description("Displays all seeds that the player has in their inventory: .seeds (page number)")]
        public async Task seedsList(CommandContext ctx,
                        [Description("The number of the page to display, defaults to 1")] int pageIndex = 1) {
            Tuple<int, List<String>> seeds = CommandsHelper.playerController.getSeeds(ctx.User.Id, pageIndex);

            String message = "\n";
            foreach(string i in seeds.Item2) {
                message += i + "\n";
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = ctx.User.Username + "'s Seeds",
                Description = message,
            }.WithFooter("page " + seeds.Item1);

            await ctx.Channel.SendMessageAsync(embed);

        }


        /// <summary>
        /// The harvest command.
        /// This will invoke the harvest mechanism on a player
        /// harvesting all harvestable outputs at the same time.
        /// Once the harvesting is finished, send an embed to the discord channel
        /// displaying what was harvested
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <returns> an embed displaying the result of the harvest </returns>
        [Command("harvest")]
        [Description("Harvests all plant pots at the same time: .harvest")]
        public async Task harvest(CommandContext ctx) {
            String result = CommandsHelper.plantPotController.harvest(ctx.User.Id);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = ctx.User.Username + "'s Harvest",
                Description = "\n" + result

            };

            await ctx.Channel.SendMessageAsync(embed);
        }

        /// <summary>
        /// Command to clear a pot of its contents
        /// </summary>
        /// <param name="ctx"> the context of the command </param>
        /// <param name="index"> the index of the pot to clear </param>
        /// <returns></returns>
        [Command("clearpot")]
        [Description("Clears a plant pot of it's contents: .clearpot (index)")]
        public async Task clear(CommandContext ctx,
                                    [Description("The plant pot you wish to clear")] int index) {

            CommandsHelper.plantPotController.clearPot(ctx.User.Id, index);

            await ctx.Channel.SendMessageAsync("Pot #" + index + " was cleared!");
        }
        
    }
}