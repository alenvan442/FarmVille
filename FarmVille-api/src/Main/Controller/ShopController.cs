using System.Globalization;
using FarmVille_api.src.Main.Model.Persistence;

namespace FarmVille_api.src.Main.Controller
{
    public class ShopController
    {
        ShopFileDAO shopFileDAO;

        /// <summary>
        /// Constructor of the shop controller
        /// </summary>
        /// <param name="shopFileDAO"> Handles data manipulation of the shop </param>
        public ShopController(ShopFileDAO shopFileDAO) {
            this.shopFileDAO = shopFileDAO;
        }
        
    }
}