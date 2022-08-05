using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Items
{
    public class Plant : Item
    {

        [JsonProperty("SeedID")]
        public uint seedID { get; private set; }

        /// <summary>
        /// Constructor for a plant
        /// </summary>
        /// <param name="name"> The name of the plant </param>
        /// <param name="amount"> The amount of plants held with this object </param>
        /// <param name="id"> The id of this plant object </param>
        /// <param name="buyPrice"> The amount a player can buy this plant for </param>
        /// <param name="sellPrice"> The amount a player can sell this plant for </param>
        public Plant(String name, int amount, uint id, double buyPrice, double sellPrice, uint seedID)
                    :base(id, amount, buyPrice, sellPrice, name) {

            this.seedID = seedID;

        }

        /// <summary>
        /// Plant cloning
        /// Utilizes an already existing plant object and create a new one
        /// based off of the original
        /// THe only noticeable difference will be in the amount of the two objects
        /// </summary>
        /// <param name="oldPlant"></param>
        /// <param name="amount"></param>
        public Plant(Plant oldPlant, int amount) 
                    :base(oldPlant.id, oldPlant.amount, oldPlant.buyPrice, oldPlant.sellPrice, oldPlant.name) {

            this.seedID = oldPlant.seedID;
        }

    }
}