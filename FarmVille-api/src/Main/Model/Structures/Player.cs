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
        [JsonProperty("OutputContainers")]
        private Dictionary<int, PlantPot> outputContainer { get; set; }
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
        public Player(ulong ID, Dictionary<int, PlantPot> OutputContainers, string Name, Dictionary<uint, Seeds> Seeds,
                        Dictionary<uint, Plant> Plants, double Balance) {
            this.UID = ID;
            this.name = Name;
            this.outputContainer = OutputContainers;
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
            this.outputContainer = new Dictionary<int, PlantPot>();
            outputContainer.Add(0, new PlantPot(0));
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
            foreach(PlantPot i in outputContainer.Values) {
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
        /// <param name="seed"></param>
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
        /// <returns> An array of strings containing each of the player's plant pots to strings </returns>
        public string[] getPotTimes() {
            string[] potTimes = new string[this.outputContainer.Count];
            foreach(PlantPot i in this.outputContainer.Values) {
                potTimes[i.id] = i.ToString();
            }
            return potTimes;
        }

        /// <summary>
        /// Retrieves a list of items in order to display the inventory
        /// </summary>
        /// <returns> An array of strings holding data of each item in the inventory </returns>
        public String[] getInventory() {
            String[] result = new string[this.inventory.Count];
            int index = 0;
            foreach(Item i in this.inventory.Values) {
                result[index] = i.ToString();
                index++;
            }
            
            return result;
        }

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
                }
            } else if(tempItem is Plant) {
                Plant tempPlant;
                this.plants.TryGetValue(item.id, out tempPlant);
                tempPlant.amount -= amount;
                if (tempPlant.amount <= 0)
                {
                    this.plants.Remove(item.id);
                }
            }
            this.inventory.Remove(item.id);

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

            Item currItem;
            if(this.inventory.ContainsKey(item.id)) {
                this.inventory.TryGetValue(item.id, out currItem);
                currItem.amount += item.amount;
            } else {
                this.inventory.Add(item.id, item);
            }

            return true;

        }

        /// <summary>
        /// Retrieves a list of seeds in order to display the seeds this user has
        /// </summary>
        /// <returns> An array of strings holding data of each seed item </returns>
        public String[] getSeeds() {
            String[] result = new string[this.seeds.Count];
            int index = 0;
            foreach (Item i in this.seeds.Values)
            {
                result[index] = i.ToString();
                index++;
            }

            return result;
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

            foreach(Output i in this.outputContainer.Values) {
                if(i.remainingTime() == TimeSpan.Zero) {
                    emptyItems.Add(i.harvest());
                }
            }

            foreach(Item i in emptyItems) {
                Item tempItem = IdentificationSearch.idSearch(i);
                this.addItem(tempItem);
                result += tempItem.amount + " " + tempItem.name + "\n";
            }

            if(emptyItems.Count == 0) {
                return "Nothing was ready to be harvested!";
            } else
            {
                return result;
            }
        }

        public Boolean purchaseItem(Item item) {
            double price = item.buyPrice * item.amount;
            if(price > this.balance) {
                return false;
            } else {
                this.balance -= price;
                this.addItem(item);
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