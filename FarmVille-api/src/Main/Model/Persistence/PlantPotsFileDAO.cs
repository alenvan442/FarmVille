namespace FarmVille_api.src.Main.Model.Persistence
{
    /// <summary>
    /// This DAO file will handle all manipulation with a specified player's plant pots
    /// Every function here will need the player to be passed in before being able to manipulate their data
    /// </summary>
    public class PlantPotsFileDAO
    {
        PlayersFileDAO playersFileDAO;

        public PlantPotsFileDAO(PlayersFileDAO playersFileDAO) {
            this.playersFileDAO = playersFileDAO;
        }
        
    }
}