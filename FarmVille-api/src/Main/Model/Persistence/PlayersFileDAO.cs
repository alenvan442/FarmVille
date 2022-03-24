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
        /// 
        /// </summary>
        /// <param name="playersJson"></param>
        /// <param name="jsonUtilities"></param>
        public PlayersFileDAO(string playersJson, JsonUtilities jsonUtilities)
        {
            this.playersJson = playersJson;
            this.jsonUtilities = jsonUtilities;
            load();
        }

        /// <summary>
        /// 
        /// </summary>
        private void load() {
            playersData = jsonUtilities.JsonDeserializeAsync<Dictionary<ulong, Player>>(playersJson).Result;
        }

        private void save() {
            jsonUtilities.JsonSerialize(playersData, playersJson);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public Player getPlayer(ulong UID) {
            Player result;
            playersData.TryGetValue(UID, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Player[] getPlayers() {
            return playersData.Values.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public Boolean addPlayer(ulong UID) {
            //TODO
            save();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public Boolean deletePlayer(ulong UID) {
            //TODO
            save();
            return false;
        }

    }
}