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
        /// 
        /// </summary>
        /// <param name="shopJson"></param>
        /// <param name="jsonUtilities"></param>
        public ShopFileDAO(string shopJson, JsonUtilities jsonUtilities) {
            this.shopJson = shopJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        /// <summary>
        /// 
        /// </summary>
        private void load() {
            shopList = jsonUtilities.JsonDeserializeAsync<List<Item>>(shopJson).Result;
        }

    }
}