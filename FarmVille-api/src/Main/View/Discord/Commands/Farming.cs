using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will consist of all commands that contribute to farming in the game, typically this will look towards 
    /// specifically the plant pots that the players hold
    /// </summary>
    public class Farming: BaseCommandModule
    {

        PlantPotController plantPotController;

        PlayerController playerController;
        EmbedUtilities embedUtilities;

        /// <summary>
        /// Constructor of the Farming commands class
        /// </summary>
        /// <param name="plantPotController"> The controller that will be handling the delegation of plant pot interactions </param>
        public Farming(PlantPotController plantPotController, PlayerController playerController, EmbedUtilities embedUtilities) {
            this.plantPotController = plantPotController;
            this.playerController = playerController;
            this.embedUtilities = embedUtilities;
        }

        /// <summary>
        /// The plant command, used for a player to plant a seed into a plant pot
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="input"> Any input the player makes, whether it is a seed name or seed id </param>
        /// <returns>  </returns>
        [Command("plant")]
        public async Task plant(CommandContext ctx, string input) {
            Player currPlayer = this.playerController.getPlayer(ctx.User.Id);

            if (input.Length <= 0 || input == null)
            {
                String[] seeds = currPlayer.getSeeds();

                //create the embed
                DiscordEmbedBuilder baseEmbed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Azure,
                    Title = ctx.User.Username + "'s Seeds"

                };

                String pageString = "";
                foreach (String i in seeds)
                {
                    pageString += i + "\n";
                }

                await ctx.Channel.SendMessageAsync("Please enter a seed to plant and use the command: /plant (seed name)");
                await this.embedUtilities.sendPagination(ctx.Channel, pageString, ctx.User, ctx.Client);
            } else {
                this.plantPotController.plantSeed(currPlayer.UID, input);
            }
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
            Player currPlayer = this.playerController.getPlayer(ctx.User.Id);
            String result = this.plantPotController.harvest(currPlayer.UID);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {

                Title = currPlayer.name + "'s Harvest",
                Description = "\n" + result

            };

            await ctx.Channel.SendMessageAsync(embed);
        }
        
    }
}