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
        /// Constructor for the plantpots DAO
        /// Setups the object through injection of two other DAO files
        /// Those two files will be utilized in manipulations of a player's plant pots
        /// </summary>
        /// <param name="playersFileDAO"></param>
        /// <param name="seedsFileDAO"></param>
        public PlantPotsFileDAO(PlayersFileDAO playersFileDAO, SeedsFileDAO seedsFileDAO) {
            this.playersFileDAO = playersFileDAO;
            this.seedsFileDAO = seedsFileDAO;
        }


        /// <summary>
        /// Plants a seed into a player's plant pot
        /// </summary>
        /// <param name="UID"> The UID of the player that requested to plant a seed </param>
        /// <param name="seedName"> The name of the seed to plant </param>
        /// <returns> A string with a message to indicate if the planting was successful </returns>
        public String plantSeed(ulong UID, string seedName) {

            Seeds currSeed = seedsFileDAO.getSeedsAmonut(seedName, 1);
            Player currPlayer = this.playersFileDAO.getPlayer(UID);

            return currPlayer.plantSeed(currSeed);
        }

        /// <summary>
        /// Plants a seed into a player's plant pot
        /// </summary>
        /// <param name="UID"> The UID of the player that requested to plant a seed </param>
        /// <param name="seedID"> The id of the seed to plant </param>
        /// <returns> A string with a message to indicate if the planting was successful </returns>
        public String plantSeed(ulong UID, uint seedID) {

            Seeds currSeed = new Seeds(this.seedsFileDAO.getSeeds(seedID), 1);
            Player currPlayer = this.playersFileDAO.getPlayer(UID);

            return currPlayer.plantSeed(currSeed);
        }

        /// <summary>
        /// Harvests a player's plant pots
        /// </summary>
        /// <param name="UID"> The id of the player who invoked the command </param>
        /// <returns> A string consisting of what was harvested </returns>
        public String harvest(ulong UID) {

            Player currPlayer = this.playersFileDAO.getPlayer(UID);
            return currPlayer.harvest();
        }


        public void clearPot(ulong UID, int index) {
            Player currPlayer = this.playersFileDAO.getPlayer(UID);
            currPlayer.clearPot(index);
        }

    }
}