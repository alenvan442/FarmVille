using FarmVille_api.src.Main.Model.Persistence;

namespace FarmVille_api.src.Main.Controller
{
    /// <summary>
    /// This is the controller when a plant pot is to be manipulated in a player's data
    /// This controller will talk to <see cref="PlantPotsFileDAO"> whilst getting information from the view
    /// </summary>
    public class PlantPotController
    {
        PlantPotsFileDAO plantPotsFileDAO;
        PlayersFileDAO playersFileDAO;

        /// <summary>
        /// Constructor for the plant pot controller
        /// </summary>
        /// <param name="plantPotsFileDAO"> A DAO file that handles data manipulation of individual plant pots </param>
        public PlantPotController(PlantPotsFileDAO plantPotsFileDAO, PlayersFileDAO playersFileDAO)
        {
            this.plantPotsFileDAO = plantPotsFileDAO;
            this.playersFileDAO = playersFileDAO;
        }

        /// <summary>
        /// Plants a seed in a player's plant pot
        /// </summary>
        /// <param name="UID"> THe ID of the player planting the seed </param>
        /// <param name="seedName"> The name of the seed being planted </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public String plantSeed(ulong UID, string seedName) {
            String result = plantPotsFileDAO.plantSeed(UID, seedName);
            this.playersFileDAO.save();
            return result;
        }

        /// <summary>
        /// Plants a seed in a player's plant pot
        /// </summary>
        /// <param name="UID"> The ID of the player planting the seed </param>
        /// <param name="seedID"> The ID of the seed being planted </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public String plantSeed(ulong UID, uint seedID) {
            String result = plantPotsFileDAO.plantSeed(UID, seedID);
            this.playersFileDAO.save();
            return result;
        }

        /// <summary>
        /// invokes the harvest command in the PlantPotsFileDAO given a player's id
        /// </summary>
        /// <param name="UID"> THe player who invoked the action </param>
        /// <returns> A message for the player in correspondence to the state of the harvest </returns>
        public String harvest(ulong UID) {
            String result = plantPotsFileDAO.harvest(UID);
            this.playersFileDAO.save();
            return result;
        }

    }
}