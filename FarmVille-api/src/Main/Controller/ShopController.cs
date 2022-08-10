using System;
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

        /// <summary>
        /// Displays items on a certain page of the shop
        /// Each page lists 10 items
        /// </summary>
        /// <param name="currPlayer"> The player requesting the command </param>
        /// <param name="pageNumber"> What page number of the shop to view </param>
        /// <returns> A string containing the items on the page in a string format </returns>
        public String shopPage(Player currPlayer, int pageNumber) {
            List<Item> page = this.shopFileDAO.getPage(pageNumber);
            String result = "\n";

            double price = 0;
            foreach(Item i in page) {
                if(i.id == 196609) {
                    price = 100 * Math.Pow(2, currPlayer.getPotsCount() - 1);
                } else {
                    price = i.buyPrice;
                }
                result += i.name;
                String priceString = price.ToString();
                String spacing = new String('.', (lineCount - i.name.Length - priceString.Length)*3);
                result += spacing;
                result += "$" + price;
                result += "\n";
            }

            return result;
        }

        /// <summary>
        /// Receives information from the discord command and initiates a purchase
        /// for the player after obtaining the necessary data
        /// </summary>
        /// <param name="player"> The player that wishes to buy an item </param>
        /// <param name="item"> The name of the item to buy </param>
        /// <param name="amount"> How many items to buy </param>
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

        /// <summary>
        /// Allows a player to sell items (Currently only for plants)
        /// </summary>
        /// <param name="player"> The player who envoked the command </param>
        /// <param name="item"> The name of the item being sold </param>
        /// <param name="amount"> The amount of the item that is to be sold </param>
        /// <returns> An item with information on what to announce in the discord channel
        ///             The name of the item: what was sold
        ///             The amount, if the amount is -1 then the player was unable to sell the item
        ///             if the amount is > 0, then the item was successfully sold </returns>
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