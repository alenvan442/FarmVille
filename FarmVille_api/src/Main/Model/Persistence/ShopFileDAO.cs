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

        /// <summary>
        /// Returns a list of items corresponding to a page in the shop
        /// Each page lists 10 items
        /// </summary>
        /// <param name="pageNumber"> the page number that is to be shown </param>
        /// <returns> a tuple containing the page number and the 
        ///             list of item, where the items are what can be 
        ///             seen on the specified page </returns>
        public Tuple<int, List<Item>> getPage(int pageNumber) {
            
            List<Item> itemCatalogue = this.shopList.Values.ToList();
            int numOfPages = (int)Math.Ceiling((double)itemCatalogue.Count / 10.0);

            if(pageNumber > numOfPages) {
                pageNumber = numOfPages;
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            int lowerBound = (pageNumber * 10) - 10;

            List<Item> items = new List<Item>();

            if (pageNumber == numOfPages)
            {
                items = itemCatalogue.GetRange(lowerBound, (itemCatalogue.Count - lowerBound));
            }
            else
            {
                items = itemCatalogue.GetRange(lowerBound, 10);
            }

            return new Tuple<int, List<Item>>(pageNumber, items);

        }

        /// <summary>
        /// The backend that handles the buy command
        /// Search and determine what item is being sought after and return it
        /// </summary>
        /// <param name="item"> the name of the item that is sought after </param>
        /// <returns> a copy of the sought after item, null if no item was found </returns>
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