using System.Globalization;
using FarmVille_api.src.Main.Model.Persistence;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Controller
{
    public class ShopController
    {
        ShopFileDAO shopFileDAO;
        PlayersFileDAO playersFileDAO;
        PlantsFileDAO plantsFileDAO;
        readonly int lineCount = 20;

        /// <summary>
        /// Constructor of the shop controller
        /// </summary>
        /// <param name="shopFileDAO"> Handles data manipulation of the shop </param>
        public ShopController(ShopFileDAO shopFileDAO, PlayersFileDAO playersFileDAO, PlantsFileDAO plantsFileDAO) {
            this.shopFileDAO = shopFileDAO;
            this.playersFileDAO = playersFileDAO;
            this.plantsFileDAO = plantsFileDAO;
        }

        public String shopPage(int pageNumber) {
            List<Item> page = this.shopFileDAO.getPage(pageNumber);
            String result = "\n";

            foreach(Item i in page) {
                result += i.name;
                String price = i.buyPrice.ToString();
                String spacing = new String('.', (lineCount - i.name.Length - price.Length)*3);
                result += spacing;
                result += "$" + i.buyPrice;
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
        public int buy(Player player, string item, int amount = 1) {
            Item? boughtItem = this.shopFileDAO.buy(item);
            if(boughtItem is null) {
                return 2;
            } else {
                boughtItem.amount = amount;
                if(player.purchaseItem(boughtItem)) {
                    this.playersFileDAO.save();
                    return 0;
                } else {
                    return 1;
                }
            }
        }

        public Item sell(Player player, string item, int amount = 1) {
            Item? soldItem = this.plantsFileDAO.getPlant(item);
            if(soldItem is null) {
                return null;
            } else {
                soldItem.amount = amount;
                if(player.sellItem(soldItem)) {
                    this.playersFileDAO.save();
                    return soldItem;
                } else
                {
                    soldItem.amount = -1;
                    return soldItem;
                }
            }
        }
        
    }
}