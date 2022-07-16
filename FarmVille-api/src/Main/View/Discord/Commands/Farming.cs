using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using FarmVille_api.src.Main.Controller;

namespace FarmVille.Commands
{
    /// <summary>
    /// This will consist of all commands that contribute to farming in the game, typically this will look towards 
    /// specifically the plant pots that the players hold
    /// </summary>
    public class Farming: BaseCommandModule
    {

        PlantPotController plantPotController;

        /// <summary>
        /// Constructor of the Farming commands class
        /// </summary>
        /// <param name="plantPotController"> The controller that will be handling the delegation of plant pot interactions </param>
        public Farming(PlantPotController plantPotController) {
            this.plantPotController = plantPotController;
        }

        /// <summary>
        /// The plant command, used for a player to plant a seed into a plant pot
        /// </summary>
        /// <param name="ctx"> The context of the command </param>
        /// <param name="input"> Any input the player makes, whether it is a seed name or seed id </param>
        /// <returns></returns>
        [Command("plant")]
        public async Task plant(CommandContext ctx, string input) {
            await Task.CompletedTask;
        }
        
    }
}