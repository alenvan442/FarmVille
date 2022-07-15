using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Items
{
    public class Plant : Item
    {

        [JsonProperty("SeedID")]
        public long seedID { get; private set; }

        public Plant(String name, int amount, long id, double buyPrice, double sellPrice)
                    :base(id, amount, buyPrice, sellPrice, name) {

        }
    }
}