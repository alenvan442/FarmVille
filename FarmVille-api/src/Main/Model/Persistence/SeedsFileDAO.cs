using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{

    /// <summary>
    /// DAO for seeds
    /// Upon startup this file will load up the json files of seeds and save it into a local dictionary
    /// from here, the seeds will be used as a template to create other seeds and return them respectively to the caller
    /// </summary>
    /// 
    /// <example>
    /// Player1 wants to buy 5 tomato seeds -> pass in that they want tomato seeds -> this class will clone the tomato seeds
    /// and sets the amount to 5 -> returns the clone
    /// </example>
    public class SeedsFileDAO
    {

        //path of the json that holds the data of the seeds
        string seedsJson;
        //collection of seeds with their IDs as the key
        Dictionary<uint, Seeds> seedsDataID;
        //collection of seeds with their names as the key
        Dictionary<string, Seeds> seedsDataString;
        //json object
        JsonUtilities jsonUtilities;

        /// <summary>
        /// Constructor for the seeds DAO object
        /// setups the DAO
        /// loads the seeds' information into a local collection
        /// </summary>
        /// <param name="seedsJson"></param>
        /// <param name="jsonUtilities"></param>
        public SeedsFileDAO(string seedsJson, JsonUtilities jsonUtilities) {

            this.seedsJson = seedsJson;
            this.jsonUtilities = jsonUtilities;
            this.seedsDataID = new Dictionary<uint, Seeds>();
            this.seedsDataString = new Dictionary<string, Seeds>();
            load();
        }

        /// <summary>
        /// Iterates through the saved files consisting of the players' data and loads them into a local database
        /// for ease of access
        /// </summary>
        private void load() {
            Dictionary<string, Seeds> tempSeeds = jsonUtilities.JsonDeserializeAsync<Dictionary<string, Seeds>>(seedsJson).Result;
            if (tempSeeds != null)
            {
                foreach (Seeds seed in tempSeeds.Values)
                {
                    this.seedsDataID.Add(seed.id, seed);
                }
                seedsDataString = tempSeeds;
            }
            Seeds tempSeed;
            seedsDataString.TryGetValue("Spinach", out tempSeed);
            tempSeed.growDuration = new TimeSpan(hours: 0, minutes: 1, seconds: 0);
            jsonUtilities.JsonSerialize<Dictionary<string, Seeds>>(seedsDataString, seedsJson);
        }

        /// <summary>
        /// Retrieves a seed based on its name
        /// </summary>
        /// <param name="name"> The name of the seed to retrieve </param>
        /// <returns> The seed object that was retrieved </returns>
        public Seeds getSeeds(string name) {
            Seeds result;
            this.seedsDataString.TryGetValue(name, out result);
            return result;
        }

        /// <summary>
        /// Retrieves a seed based on its id
        /// </summary>
        /// <param name="ID"> The id of the seed to retrieve </param>
        /// <returns> The seed object that was retrieved </returns>
        public Seeds getSeeds(uint ID) {
            Seeds result;
            seedsDataID.TryGetValue(ID, out result);
            return result;
        }

        /// <summary>
        /// Creates and returns a seed object with an amount > 1
        /// </summary>
        /// <param name="name"> The name of the seed to create </param>
        /// <param name="amount"> The number of seeds to create </param>
        /// <returns> A new seed object with the correct properties and amount </returns>
        public Seeds getSeedsAmonut(string name, int amount = 1) {
            Seeds temp;
            temp = this.getSeeds(name);
            Seeds result = new Seeds(temp, amount);
            return result;
        }

        /// <summary>
        /// Creates and returns a seed object with an amount > 1
        /// </summary>
        /// <param name="ID"> The id of the seed to create </param>
        /// <param name="amount"> The number of seeds to create </param>
        /// <returns> A new seed object with the correct properties and amount </returns>
        public Seeds getSeedsAmonut(uint ID, int amount = 1) {
            
            Seeds temp;
            temp = this.getSeeds(ID);
            Seeds result = new Seeds(temp, amount);
            return result;
        }
        
    }
}