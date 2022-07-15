using System.Collections;
using DSharpPlus.Entities;
using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Structures.Outputs;

namespace FarmVille_api.src.Main.Model.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public class Player
    {

        public ulong UID { get; private set; }
        private Dictionary<int, PlantPot> outputContainer { get; set; }

        public string name { get; private set; }
        private Dictionary<long, Seeds> inventory { get; set; }
        private long balance { get; set; }


        /// <summary>
        /// Constructor for a new player
        /// Give them the first plant pot for free
        /// </summary>
        public Player(DiscordMember member) {
            this.UID = member.Id;
            this.name = member.Username;
            outputContainer.Add(0, new PlantPot(0));
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

        public Boolean addItem(Seeds seed) {
            Seeds prevSeed;
            if(this.inventory.TryGetValue(seed.id, out prevSeed)) {
                prevSeed.amount += seed.amount;
                return this.inventory.TryAdd(seed.id, prevSeed);
            } else
            {
                this.inventory.Add(seed.id, seed);
                return true;
            }
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