using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class ShopFileDAO
    {

        List<Items> shopList;
        string shopJson;
        JsonUtilities jsonUtilities;

        public ShopFileDAO(string shopJson, JsonUtilities jsonUtilities) {
            this.shopJson = shopJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        private void load() {
            shopList = jsonUtilities.JsonDeserializeAsync<List<Items>>(shopJson).Result;
        }

    }
}