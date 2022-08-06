using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class ShopFileDAO
    {

        //List<Item> shopList;
        Dictionary<string, Item> shopList;
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
            this.shopList = new Dictionary<string, Item>();
            load();
        }

        /// <summary>
        /// Loads the shop's data into a more local place for ease of access
        /// </summary>
        private void load() {
            shopList = jsonUtilities.JsonDeserializeAsync<Dictionary<string, Item>>(shopJson).Result;
        }

        public List<Item> getPage(int pageNumber) {
            int lowerBound = (pageNumber * 10) - 10;
            List<Item> itemCatalogue = this.shopList.Values.ToList();
            int numOfPages = (int)Math.Ceiling((double)itemCatalogue.Count / 10.0);

            if(pageNumber > numOfPages) {
                pageNumber = numOfPages;
            }

            return itemCatalogue.GetRange(lowerBound, 10);

        }

        public Item? buy(string item) {
            Item? tempItem;
            this.shopList.TryGetValue(item, out tempItem);

            if(tempItem is null) {
                return null;
            } else {
                return IdentificationSearch.idSearch(tempItem);
            }

        }

    }
}