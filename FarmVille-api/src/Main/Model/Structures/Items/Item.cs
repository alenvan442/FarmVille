using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Items
{
    public class Item
    {

        [JsonProperty("Name")]
        public string name { get; private set; }
        [JsonProperty("ID")]
        public uint id { get; private set; }
        [JsonProperty("Amount")]
        public int amount;
        [JsonProperty("BuyPrice")]
        public double buyPrice { get; private set; }
        [JsonProperty("SellPrice")]
        public double sellPrice { get; private set; }

        /// <summary>
        /// The constructor for an item
        /// </summary>
        /// <param name="id"> The id of a specific item </param>
        /// <param name="amount"> How many of the items are present </param>
        /// <param name="buyPrice"> How much to buy the item for </param>
        /// <param name="sellPrice"> How much one can sell the item for </param>
        /// <param name="name"> The name of the item </param>
        public Item(uint id, int amount, double buyPrice, double sellPrice, string name) {
            this.id = id;
            this.amount = amount;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.name = name;
        }

        /// <summary>
        /// To string method for an item
        /// Format: (Item Name)              (Item Amount)
        /// </summary>
        /// <returns> A string consisting of the data of an item </returns>
        public override string ToString()
        {
            return this.name + "        " + this.amount;
        }

    }
}