using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class ShopFileDAO
    {

        string shopJson;
        JsonUtilities jsonUtilities;

        public ShopFileDAO(string shopJson, JsonUtilities jsonUtilities) {
            this.shopJson = shopJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        public void load() {

        }

    }
}