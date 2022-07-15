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

        public Farming(PlantPotController plantPotController) {
            this.plantPotController = plantPotController;
        }

        [Command("plant")]
        public async Task plant(CommandContext ctx, string input) {
            await Task.CompletedTask;
        }
        
    }
}