using System.Runtime.CompilerServices;
using System.Collections;
using DSharpPlus.Entities;
using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Structures.Outputs;
using FarmVille_api.src.Main.Model.Utilities;
using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public class Player
    {

        [JsonProperty("ID")]
        public ulong UID { get; private set; }
        [JsonProperty("Pots")]
        private List<PlantPot> plantPots { get; set; }
        [JsonProperty("Name")]
        public string name { get; private set; }
        [JsonProperty("Seeds")]
        private Dictionary<uint, Seeds> seeds { get; set; }
        [JsonProperty("Plants")]
        private Dictionary<uint, Plant> plants { get; set; }
        [JsonProperty("Balance")]
        private double balance { get; set; }

        private Dictionary<long, Item> inventory;

        [JsonConstructor]
        public Player(ulong ID, List<PlantPot> plantPots, string Name, Dictionary<uint, Seeds> Seeds,
                        Dictionary<uint, Plant> Plants, double Balance) {
            this.UID = ID;
            this.name = Name;
            this.plantPots = plantPots;
            this.balance = Balance;
            this.seeds = Seeds;
            this.plants = Plants;
            this.inventory = new Dictionary<long, Item>();
            loadInventory();
        }

        /// <summary>
        /// Constructor for a new player
        /// Give them the first plant pot for free
        /// </summary>
        public Player(DiscordMember member) {
            this.UID = member.Id;
            this.name = member.Username;
            this.plantPots = new List<PlantPot>() {new PlantPot()};
            this.balance = 5.00;
            this.seeds = new Dictionary<uint, Seeds>();
            this.plants = new Dictionary<uint, Plant>();
            this.inventory = new Dictionary<long, Item>();
            loadInventory();
        }

        /// <summary>
        /// Loads the inventory
        /// This is utilized to bypass the json downside of utilizing inheritance
        /// Upon player load, seeds and plants will be loaded into their respected dictionaries
        /// In order to maintain their child type
        /// Afterwards are loaded into one singular dictionary for further use
        /// </summary>
        private void loadInventory() {
            foreach(Seeds i in this.seeds.Values) {
                this.inventory.Add(i.id, i);
            } 
            foreach(Plant i in this.plants.Values) {
                this.inventory.Add(i.id, i);
            }
        }

        /// <summary>
        /// Finds the first empty plant pot and returns it
        /// If no plant pot is found to be empty, then return a null
        /// </summary>
        /// <returns></returns>
        private PlantPot findFirstEmptyPot() {
            foreach(PlantPot i in plantPots) {
                if(i.isEmpty()) {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        /// Plants the inputted seed into the first empty plant pot
        /// that this player has.
        /// Return a true if the plant was successful, false otherwise.
        /// </summary>
        /// <param name="seed"> The seed that is to be planted </param>
        /// <returns></returns>
        public String plantSeed(Seeds seed) {
            PlantPot target = this.findFirstEmptyPot();
            if (target == null)
            {
                return "All your plant pots are full!";
            } else {
                target.plantSeed(seed);
                this.removeItem(seed, 1);
                return "Successfully planted your " + seed.name;
            }
        }

        /// <summary>
        /// Gets a collective view of all of the player's plant pot's data
        /// Each of these data includes the plant pot's number
        /// What the pot is currently growing
        /// As well as the remaining time left on the plant pot
        /// </summary>
        /// <param name="pageIndex"> the page number that is to be shown </param>
        /// <returns> An array of strings containing each of the player's plant pots to strings </returns>
        public List<String> getPots(int pageNumber) {
            List<String> potData = new List<string>();
            foreach(PlantPot i in this.plantPots) {
                potData.Add(i.ToString(potData.Count+1));
            }
            
            int numOfPages = (int)Math.Ceiling((double)potData.Count / 4.0);

            if(pageNumber > numOfPages) {
                pageNumber = numOfPages;
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            
            int lowerBound = (4 * pageNumber) - 4;

            if (pageNumber == numOfPages)
            {
                return potData.GetRange(lowerBound, (potData.Count - lowerBound));
            } else if(potData.Count() == 0) {
                return potData;
            }
            else
            {
                return potData.GetRange(lowerBound, 4);
            }
        }

        /// <summary>
        /// Returns the number of plant pots the player currently holds
        /// </summary>
        /// <returns> an integer representing their plant pot count </returns>
        public int getPotsCount() {
            return this.plantPots.Count();
        }

        /// <summary>
        /// Retrieves a list of items in order to display the inventory
        /// </summary>
        /// <param name="pageIndex"> The page of the inventory that is to be shown </param>
        /// <returns> An array of strings holding data of each item in the inventory </returns>
        public List<String> getInventory(int pageIndex) {
            List<String> items = new List<string>();
            List<String> results = new List<string>();

            foreach(Item i in this.inventory.Values) {
                items.Add(i.ToString());
            }

            int numOfPages = (int)Math.Ceiling((double)items.Count / 10);

            if(pageIndex > numOfPages) {
                pageIndex = numOfPages;
            }

            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            int lowerBound = (10 * pageIndex) - 10;

            results.Add(pageIndex.ToString());

            if (pageIndex == numOfPages)
            {
                results.AddRange(items.GetRange(lowerBound, (items.Count - lowerBound)));
            } else if(items.Count == 0) {
                return results;
            } else {
                results.AddRange(items.GetRange(lowerBound, 10));
            }
            return results;
        }

        /// <summary>
        /// Removes an item from the player's inventory
        /// </summary>
        /// <param name="item"> The item that is to be removed </param>
        /// <param name="amount"> how many of the item is to be removed, defaults to all </param>
        /// <returns> a boolean indicating if the action was sucessful </returns>
        public Boolean removeItem(Item item, int amount = int.MaxValue) {
            if(item is null) {
                return false;
            }

            Item tempItem = IdentificationSearch.idSearch(item); 

            if(tempItem is Seeds) {
                Seeds tempSeed;
                this.seeds.TryGetValue(item.id, out tempSeed);
                tempSeed.amount -= amount;
                if(tempSeed.amount <= 0) {
                    this.seeds.Remove(item.id);
                    this.inventory.Remove(item.id);
                }
            } else if(tempItem is Plant) {
                Plant tempPlant;
                this.plants.TryGetValue(item.id, out tempPlant);
                tempPlant.amount -= amount;
                if (tempPlant.amount <= 0)
                {
                    this.plants.Remove(item.id);
                    this.inventory.Remove(item.id);
                }
            }

            return true;
        }

        /// <summary>
        /// Adds an item to the player's inventory
        /// We check to see what child type the item is
        /// Determine whether or not the item already exists in the player's inventory
        ///     If it does: Add the amount of the input to the already existing item
        ///     If not: Add the new item to the inventory
        /// 
        /// The item is added twice, both to the specified child type's dictionary as well as the overall inventory
        /// </summary>
        /// <param name="item"> The item being added </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public Boolean addItem(Item item) {

            if(item is null) {
                return false;
            }

            Item tempItem = IdentificationSearch.idSearch(item);
            
            if(tempItem is Seeds) {
                Seeds pastSeed;
                if(this.seeds.ContainsKey(tempItem.id)) {
                    this.seeds.TryGetValue(tempItem.id, out pastSeed);
                    pastSeed.amount += tempItem.amount;
                } else {
                    this.seeds.Add(tempItem.id, (Seeds)tempItem);
                }
            } else if(tempItem is Plant) {
                Plant pastPlant;
                if(this.plants.ContainsKey(tempItem.id)) {
                    this.plants.TryGetValue(tempItem.id, out pastPlant);
                    pastPlant.amount += tempItem.amount;
                } else {
                    this.plants.Add(tempItem.id, (Plant)tempItem);
                }
            }

            if (!this.inventory.ContainsKey(item.id))
            {
                this.inventory.Add(item.id, tempItem);
            }

            return true;

        }

        /// <summary>
        /// Retrieves a list of seeds in order to display the seeds this user has
        /// </summary>
        /// <returns> An array of strings holding data of each seed item </returns>
        public List<String> getSeeds(int pageIndex) {
            List<String> result = new List<string>();
            foreach (Item i in this.seeds.Values)
            {
                result.Add(i.ToString());
            }
    
            int numOfPages = (int)Math.Ceiling((double)result.Count / 10);

            if(pageIndex > numOfPages) {
                pageIndex = numOfPages;
            }

            if(pageIndex == 0) {
                pageIndex = 1;
            }
            
            int lowerBound = (10 * pageIndex) - 10;

            if (pageIndex == numOfPages)
            {
                return result.GetRange(lowerBound, (result.Count - lowerBound));
            } else if(result.Count() == 0) {
                return result;
            }
            else
            {
                return result.GetRange(lowerBound, 10);
            }
        }


        /// <summary>
        /// Iterates through each output container associated
        /// with the player and check to see if it is ready to be harvested
        /// if so harvest then add the harvested output into the player's inventory
        /// </summary>
        /// <returns> A string the contains a list of what was harvested </returns>
        public String harvest() {

            List<Item> emptyItems = new List<Item>();
            String result = "";

            foreach(PlantPot i in this.plantPots) {
                if(i.remainingTime() == TimeSpan.Zero) {
                    emptyItems.Add(i.harvest());
                }
            }

            foreach(Item i in emptyItems) {
                Item tempItem = IdentificationSearch.idSearch(i);
                this.addItem(tempItem);
                if(tempItem.name == "Cake") {
                    result += tempItem.amount + " Happy Birthday!" + "\n";
                } else
                {
                    result += tempItem.amount + " " + tempItem.name + "\n";
                }
            }

            if(emptyItems.Count == 0) {
                return "Nothing was ready to be harvested!";
            } else
            {
                return result;
            }
        }

        /// <summary>
        /// The player's method that handles the purchasing command
        /// </summary>
        /// <param name="item"> The item that is to be purchased </param>
        /// <returns> a boolean indicating whether or not the purchase was successful </returns>
        public Boolean purchaseItem(Item item) {
            double price = 0;
            if(item is PlantPot) {
                price = 100 * Math.Pow(2, this.getPotsCount() - 1);
                if (price > this.balance)
                {
                    return false;
                }
                else
                {
                    this.plantPots.Add(new PlantPot());
                }
            } else
            {
                price = item.buyPrice * item.amount;

                if (price > this.balance)
                {
                    return false;
                }
                else
                {
                    this.addItem(item);
                }
            }
            this.balance -= price;
            return true;
        }

        /// <summary>
        /// The player method that handles selling an item that they have in their
        /// inventory
        /// </summary>
        /// <param name="item"> The item that is to be sold </param>
        /// <returns> a boolean representing if the player was able to sell the item </returns>
        public Boolean sellItem(Item item) {
            Item beingSold;
            this.inventory.TryGetValue(item.id, out beingSold);

            if(beingSold.amount < item.amount || beingSold is null) {
                return false;
            } else {
                beingSold.amount -= item.amount;
                this.balance += beingSold.sellPrice * item.amount;
                if(beingSold.amount == 0) {
                    this.removeItem(beingSold);
                }
                return true;
            }
        }


        /// <summary>
        /// The toString method of the player class
        /// Format: 
        ///         (Player's Name)
        /// 
        ///         (Player's Balance)
        /// </summary>
        /// <returns></returns>
        public override string ToString() {

            String result = "";
            result += "\nUsername: " + this.name + "\n\n";
            result += "Balance: " + this.balance;

            return result;
        }

    }
}