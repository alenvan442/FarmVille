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
        private Dictionary<long, Seeds> seeds { get; set; }
        [JsonProperty("Plants")]
        private Dictionary<long, Plant> plants { get; set; }
        [JsonProperty("Balance")]
        private double balance { get; set; }


        private Dictionary<long, Item> inventory;

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
            this.seeds = new Dictionary<long, Seeds>();
            this.plants = new Dictionary<long, Plant>();
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
            
            if(item is Seeds) {
                Seeds? newSeed = (Seeds)item;
                if(this.seeds.ContainsKey(item.id)) {
                    this.seeds.TryGetValue(item.id, out newSeed);
                    newSeed.amount += item.amount;
                    this.seeds.Add(item.id, newSeed);
                } else {
                    this.seeds.Add(newSeed.id, newSeed);
                }
            } else if(item is Plant) {
                Plant? newPlant = (Plant)item;
                if(this.plants.ContainsKey(item.id)) {
                    this.plants.TryGetValue(item.id, out newPlant);
                    newPlant.amount += item.amount;
                    this.plants.Add(item.id, newPlant);
                } else {
                    this.plants.Add(newPlant.id, newPlant);
                }
            }

            Item? currItem;
            if(this.inventory.ContainsKey(item.id)) {
                this.inventory.TryGetValue(item.id, out currItem);
                currItem.amount += item.amount;
                this.inventory.Add(item.id, currItem);
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
            result += "\n" + this.name + "\n\n";
            result += this.balance;

            return result;
        }

    }
}