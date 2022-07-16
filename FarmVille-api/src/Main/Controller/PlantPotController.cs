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

        /// <summary>
        /// Constructor for the plant pot controller
        /// </summary>
        /// <param name="plantPotsFileDAO"> A DAO file that handles data manipulation of individual plant pots </param>
        public PlantPotController(PlantPotsFileDAO plantPotsFileDAO)
        {
            this.plantPotsFileDAO = plantPotsFileDAO;
        }

        /// <summary>
        /// Plants a seed in a player's plant pot
        /// </summary>
        /// <param name="UID"> THe ID of the player planting the seed </param>
        /// <param name="seedName"> The name of the seed being planted </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public Boolean plantSeed(ulong UID, string seedName) {
            return plantPotsFileDAO.plantSeed(UID, seedName);
        }

        /// <summary>
        /// Plants a seed in a player's plant pot
        /// </summary>
        /// <param name="UID"> The ID of the player planting the seed </param>
        /// <param name="seedID"> The ID of the seed being planted </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public Boolean plantSeed(ulong UID, long seedID)
        {
            return plantPotsFileDAO.plantSeed(UID, seedID);
        }

        /// <summary>
        /// Retrieves the data of a player's plant pots
        /// </summary>
        /// <param name="UID"> The ID of the player who requested this retrieval </param>
        /// <returns> An array of strings consisting of the data of the plant pots </returns>
        public string[] getPotData(ulong UID) {
            return plantPotsFileDAO.getPotData(UID);
        }

    }
}