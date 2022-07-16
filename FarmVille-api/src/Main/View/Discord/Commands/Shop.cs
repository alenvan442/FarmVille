using FarmVille_api.src.Main.Controller;

namespace FarmVille.Commands
{
    ///
    /// <summary>
    /// This will hold all the neccessary commands that corresponds to player's interactions with the shop
    /// </summary>
    public class Shop
    {

        ShopController shopController;

        /// <summary>
        /// Constructor of the shop commands
        /// </summary>
        /// <param name="shopController"> The class in charge of the delegation of shop related tasks </param>
        public Shop(ShopController shopController) {
            this.shopController = shopController;
        }
        
    }
}