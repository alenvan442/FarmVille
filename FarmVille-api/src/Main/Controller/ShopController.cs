using System.Globalization;
using FarmVille_api.src.Main.Model.Persistence;

namespace FarmVille_api.src.Main.Controller
{
    public class ShopController
    {
        ShopFileDAO shopFileDAO;
        public ShopController(ShopFileDAO shopFileDAO) {
            this.shopFileDAO = shopFileDAO;
        }
        
    }
}