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
        Dictionary<long, Seeds> seedsData;
        JsonUtilities jsonUtilities;

        public SeedsFileDAO(string seedsJson, JsonUtilities jsonUtilities)
        {
            this.seedsJson = seedsJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        private void load() {
            seedsData = jsonUtilities.JsonDeserializeAsync<Dictionary<long, Seeds>>(seedsJson).Result;
        }
        
    }
}