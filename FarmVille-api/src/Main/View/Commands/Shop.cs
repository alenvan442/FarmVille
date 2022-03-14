using FarmVille_api.src.Main.Controller;

namespace FarmVille_api.src.Main.View.Commands
{
    /// <summary>
    /// This will hold all the neccessary commands that corresponds to player's interactions with the shop
    /// </summary>
    public class Shop
    {

        ShopController shopController;

        public Shop(ShopController shopController) {
            this.shopController = shopController;
        }
        
    }
}