using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class ShopFileDAO
    {

        List<Item> shopList;
        string shopJson;
        JsonUtilities jsonUtilities;

        /// <summary>
        /// Constructor of the shop DAO class
        /// </summary>
        /// <param name="shopJson"> The string that points to where the shopJson file is being held </param>
        /// <param name="jsonUtilities"> A class that handles json manipulations </param>
        public ShopFileDAO(string shopJson, JsonUtilities jsonUtilities) {
            this.shopJson = shopJson;
            this.jsonUtilities = jsonUtilities;
            this.shopList = new List<Item>();
            load();
        }

        /// <summary>
        /// Loads the shop's data into a more local place for ease of access
        /// </summary>
        private void load() {
            shopList = jsonUtilities.JsonDeserializeAsync<List<Item>>(shopJson).Result;
        }

    }
}