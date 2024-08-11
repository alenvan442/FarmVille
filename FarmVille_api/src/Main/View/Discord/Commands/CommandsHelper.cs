using System.Globalization;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.View.Discord.Commands
{
    public static class CommandsHelper
    {
        public static PlantPotController plantPotController;

        public static PlayerController playerController;
        public static ShopController shopController;

        /// <summary>
        /// Constructor of the Farming commands class
        /// </summary>
        /// <param name="plantPotController"> The controller that will be handling the delegation of plant pot interactions </param>
        public static void setup(PlantPotController plantPotController1, PlayerController playerController1,
                                    ShopController shopController1)
        {
            plantPotController = plantPotController1;
            playerController = playerController1;
            shopController = shopController1;
        }
    }
}