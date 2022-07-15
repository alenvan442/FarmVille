using System.Collections;
using DSharpPlus.Entities;
using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Structures.Outputs;
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
            outputContainer.Add(0, new PlantPot(0));
            this.balance = 5.00;
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
        public Boolean plantSeed(Seeds seed) {
            PlantPot target = this.findFirstEmptyPot();
            if (target == null)
            {
                return false;
            } else {
                target.plantSeed(seed);
                return true;
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

        public String[] getInventory() {
            String[] result = new string[this.inventory.Count];
            int index = 0;
            foreach(Item i in this.inventory.Values) {
                result[index] = i.ToString();
                index++;
            }
            
            return result;
        }

        public Boolean addItem(Item item) {

            if(item is null) {
                return false;
            }
            
            if(item is Seeds) {
                Seeds newSeed = (Seeds)item;
                if(this.seeds.ContainsKey(item.id)) {
                    this.seeds.TryGetValue(item.id, out newSeed);
                    newSeed.amount += item.amount;
                    this.seeds.Add(item.id, newSeed);
                } else {
                    this.seeds.Add(newSeed.id, newSeed);
                }
            } else if(item is Plant) {
                Plant newPlant = (Plant)item;
                if(this.plants.ContainsKey(item.id)) {
                    this.plants.TryGetValue(item.id, out newPlant);
                    newPlant.amount += item.amount;
                    this.plants.Add(item.id, newPlant);
                } else {
                    this.plants.Add(newPlant.id, newPlant);
                }
            }

            Item currItem;
            if(this.inventory.ContainsKey(item.id)) {
                this.inventory.TryGetValue(item.id, out currItem);
                currItem.amount += item.amount;
                this.inventory.Add(item.id, currItem);
            } else {
                this.inventory.Add(item.id, item);
            }

            return true;

        }

        public override string ToString()
        {
            String result = "";
            result += "\n" + this.name + "\n\n";
            result += this.balance;

            return result;
        }

    }
}