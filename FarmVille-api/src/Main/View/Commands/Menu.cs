using DSharpPlus.CommandsNext;
using FarmVille_api.src.Main.Controller;

namespace FarmVille_api.src.Main.View.Commands
{
    /// <summary>
    /// This will hold all basic menu commands such as help, shop, inventory etc.
    /// </summary>
    public class Menu: BaseCommandModule
    {

        PlayerController playerController;
        public Menu(PlayerController playerController) {
            this.playerController = playerController;
        }
        
    }
}