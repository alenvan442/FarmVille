using DSharpPlus.Entities;
using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    /// <summary>
    /// This DAO file will handle all player data in terms of loading, saving, creating, and deleting player data
    /// </summary>
    public class PlayersFileDAO
    {

        Dictionary<ulong, Player> playersData;
        string playersJson;
        JsonUtilities jsonUtilities;

        /// <summary>
        /// Constructor for the PlayersFileDAO object
        /// setups the DAO object
        /// loads the player datas
        /// </summary>
        /// <param name="playersJson"> The path to the json file </param>
        /// <param name="jsonUtilities"> An object that helps with json manipulations </param>
        public PlayersFileDAO(string playersJson, JsonUtilities jsonUtilities)
        {
            this.playersJson = playersJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        /// <summary>
        /// Deserializes the players json file into a collection of players and their data
        /// The data is then saved into a local collection for easier access
        /// </summary>
        private void load() {
            playersData = jsonUtilities.JsonDeserializeAsync<Dictionary<ulong, Player>>(playersJson).Result;
        }

        /// <summary>
        /// Serializes the local collection of player data into the json file
        /// This acts as saving player data
        /// </summary>
        private void save() {
            jsonUtilities.JsonSerialize(playersData, playersJson);
        }

        /// <summary>
        /// Retrieves a specific player based on their UID
        /// </summary>
        /// <param name="UID"> The UID of the player to be receieved </param>
        /// <returns> The player associated with the UID, null if not found </returns>
        public Player getPlayer(ulong UID) {
            Player result;
            playersData.TryGetValue(UID, out result);
            return result;
        }

        /// <summary>
        /// Gets a collection of all players currently in the database
        /// </summary>
        /// <returns> An array of all players that are saved in the database </returns>
        public Player[] getPlayers() {
            return playersData.Values.ToArray();
        }

        /// <summary>
        /// Adds a new player
        /// Creates a new player and adds them to the local collection then saves
        /// </summary>
        /// <param name="UID"> The UID of the player to add </param>
        /// <returns> A boolean to indicate whether or not the player was successfully created </returns>
        public Boolean addPlayer(ulong UID) {
            //Checks to see if the UID is already in the system
            if(playersData.TryGetValue(UID, out _)) {
                return false;
            } else
            {
                Player newPlayer = new Player(UID);
                playersData.Add(UID, newPlayer);
                save();
                return true;
            }
        }

        /// <summary>
        /// Deletes a player from the database, erasing their data
        /// </summary>
        /// <param name="UID"> The UID of the player to delete </param>
        /// <returns> Returns a boolean indicating whether or not the deletion was successful </returns>
        public Boolean deletePlayer(ulong UID) {
            playersData.Remove(UID);
            save();
            return true;
        }

    }
}