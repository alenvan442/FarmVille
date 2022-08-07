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
        public async Task plant(CommandContext ctx, string input = "") {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);

            if (input?.Length <= 0 || input == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a seed to plant and use the command: /plant (seed name)\n" +
                                                    "Or use the command: /seeds (page number), to view your list of seeds");
            } else {
                input = input[0].ToString().ToUpper() + input.Substring(1);
                String result = CommandsHelper.plantPotController.plantSeed(currPlayer.UID, input);

                await ctx.Channel.SendMessageAsync(result);
            }
        }

        [Command("seeds")]
        public async Task seedsList(CommandContext ctx, int pageIndex = 1) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            List<String> seeds = currPlayer.getSeeds(pageIndex);

            String message = "\n";
            foreach(string i in seeds) {
                message += i + "\n";
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Title = ctx.User.Username + "'s Seeds",
                Description = message,
            }.WithFooter("page " + pageIndex);

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
        public async Task harvest(CommandContext ctx) {
            Player currPlayer = CommandsHelper.playerController.getPlayer(ctx.User.Id);
            String result = CommandsHelper.plantPotController.harvest(currPlayer.UID);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = currPlayer.name + "'s Harvest",
                Description = "\n" + result

            };

            await ctx.Channel.SendMessageAsync(embed);
        }
        
    }
}