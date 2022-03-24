using FarmVille_api.src.Main.Model.Structures;
using FarmVille_api.src.Main.Model.Structures.Items;

namespace FarmVille_api.src.Main.Model.Persistence
{
    /// <summary>
    /// This DAO file will handle all manipulation with a specified player's plant pots
    /// Every function here will need the player to be passed in before being able to manipulate their data
    /// </summary>
    public class PlantPotsFileDAO
    {
        PlayersFileDAO playersFileDAO;
        SeedsFileDAO seedsFileDAO;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playersFileDAO"></param>
        /// <param name="seedsFileDAO"></param>
        public PlantPotsFileDAO(PlayersFileDAO playersFileDAO, SeedsFileDAO seedsFileDAO) {
            this.playersFileDAO = playersFileDAO;
            this.seedsFileDAO = seedsFileDAO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="seedName"></param>
        /// <returns></returns>
        public Boolean plantSeed(ulong UID, string seedName) {
            Seeds currSeed = new Seeds(this.seedsFileDAO.getSeeds(seedName), 1);
            Player currPlayer = this.playersFileDAO.getPlayer(UID);

            return currPlayer.plantSeed(currSeed);
        }

        public TimeSpan getRemainingTime(ulong UID, int potIndex) {
            Player currPlayer = this.playersFileDAO.getPlayer(UID);
            return currPlayer.getPotTime(potIndex);
        }
        
    }
}