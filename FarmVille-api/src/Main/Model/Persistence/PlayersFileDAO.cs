using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    /// <summary>
    /// This DAO file will handle all player data in terms of loading, saving, creating, and deleting player data
    /// </summary>
    public class PlayersFileDAO
    {

        string playersJson;
        JsonUtilities jsonUtilities;

        public PlayersFileDAO(string playersJson, JsonUtilities jsonUtilities)
        {
            this.playersJson = playersJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        public void load() {

        }
        
    }
}