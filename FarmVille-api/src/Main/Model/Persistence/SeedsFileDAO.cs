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

        string seedsJson;
        Dictionary<long, Seeds> seedsDataID;
        Dictionary<string, Seeds> seedsDataString;
        JsonUtilities jsonUtilities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seedsJson"></param>
        /// <param name="jsonUtilities"></param>
        public SeedsFileDAO(string seedsJson, JsonUtilities jsonUtilities)
        {
            this.seedsJson = seedsJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        /// <summary>
        /// 
        /// </summary>
        private void load() {
            seedsDataID = jsonUtilities.JsonDeserializeAsync<Dictionary<long, Seeds>>(seedsJson).Result;
            foreach(Seeds seed in seedsDataID.Values) {
                seedsDataString.Add(seed.name, seed);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Seeds getSeeds(string name) {
            Seeds result;
            seedsDataString.TryGetValue(name, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Seeds getSeeds(long ID) {
            Seeds result;
            seedsDataID.TryGetValue(ID, out result);
            return result;
        }
        
    }
}