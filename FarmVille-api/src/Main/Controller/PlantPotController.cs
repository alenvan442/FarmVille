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
        public PlantPotController(PlantPotsFileDAO plantPotsFileDAO)
        {
            this.plantPotsFileDAO = plantPotsFileDAO;
        }
    }
}