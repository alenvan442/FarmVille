using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Items
{
    public class Plant : Item
    {

        [JsonProperty("SeedID")]
        public long seedID { get; private set; }

        /// <summary>
        /// Constructor for a plant
        /// </summary>
        /// <param name="name"> The name of the plant </param>
        /// <param name="amount"> The amount of plants held with this object </param>
        /// <param name="id"> The id of this plant object </param>
        /// <param name="buyPrice"> The amount a player can buy this plant for </param>
        /// <param name="sellPrice"> The amount a player can sell this plant for </param>
        public Plant(String name, int amount, long id, double buyPrice, double sellPrice)
                    :base(id, amount, buyPrice, sellPrice, name) {

        }
    }
}