using System.Globalization;
using FarmVille_api.src.Main.Model.Persistence;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Structures.Items;

namespace FarmVille_api.src.Main.Controller
{
    public class ShopController
    {
        ShopFileDAO shopFileDAO;
        readonly int lineCount = 20;

        /// <summary>
        /// Constructor of the shop controller
        /// </summary>
        /// <param name="shopFileDAO"> Handles data manipulation of the shop </param>
        public ShopController(ShopFileDAO shopFileDAO) {
            this.shopFileDAO = shopFileDAO;
        }

        public String shopPage(int pageNumber) {
            List<Item> page = this.shopFileDAO.getPage(pageNumber);
            String result = "\n";

            foreach(Item i in page) {
                result += i.name;
                String price = i.buyPrice.ToString();
                result += Enumerable.Repeat(" ", lineCount - i.name.Length - price.Length);
                result.Concat(price);
                result += "\n";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns> 0 for success
        ///           1 for insufficient balance 
        ///           2 for unknown item </returns>
        public int buy(Player player, string item) {
            Item? boughtItem = this.shopFileDAO.buy(item);
            if(boughtItem is null) {
                return 2;
            } else {
                if(player.purchaseItem(boughtItem)) {
                    return 0;
                } else {
                    return 1;
                }
            }
        }
        
    }
}