using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using FarmVille_api.src.Main.Controller;

namespace FarmVille.Commands
{
    ///
    /// <summary>
    /// This will hold all the neccessary commands that corresponds to player's interactions with the shop
    /// </summary>
    public class Shop: BaseCommandModule
    {

        [Command("shop")]
        public async Task shop(CommandContext ctx, int pageNumber = 1) {
            
        }
        
    }
}