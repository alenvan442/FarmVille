using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    /// <summary>
    /// This DAO file will handle all player data in terms of loading, saving, creating, and deleting player data
    /// </summary>
    public class PlayersFileDAO
    {

        Dictionary<long, Player> playersData;
        string playersJson;
        JsonUtilities jsonUtilities;

        public PlayersFileDAO(string playersJson, JsonUtilities jsonUtilities)
        {
            this.playersJson = playersJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        private void load() {
            playersData = jsonUtilities.JsonDeserializeAsync<Dictionary<long, Player>>(playersJson).Result;
        }
        
    }
}