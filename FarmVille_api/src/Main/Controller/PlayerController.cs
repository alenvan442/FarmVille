using DSharpPlus.Entities;
using FarmVille.FarmVille_api.src.Main.Model.Persistence;
using FarmVille.FarmVille_api.src.Main.Model.Structures;

namespace FarmVille.FarmVille_api.src.Main.Controller
{
    /// <summary>
    /// This controller will receive information from the view and either add, delete, or update a player accordingly
    /// The main purpose of this controller is to receive and delegate tasks that involves the creation or deletion of a player
    /// </summary>
    public class PlayerController
    {
        PlayersFileDAO playersFileDAO;

        /// <summary>
        /// Constructor for the player controller
        /// Utilizes the player DAO
        /// </summary>
        /// <param name="playersFileDAO"> A class that holds methods that correspond with the manipulation of data with players </param>
        public PlayerController(PlayersFileDAO playersFileDAO) {
            this.playersFileDAO = playersFileDAO;
        }

        /// <summary>
        /// Retrieves a player from the database given an ID
        /// </summary>
        /// <param name="UID"> The ID of the player to look for </param>
        /// <returns> The retrieved player </returns>
        public Player getPlayer(ulong UID) {
            return playersFileDAO.getPlayer(UID);
        }

        /// <summary>
        /// Retrieves all of the players in the database
        /// </summary>
        /// <returns> An array of all of the players </returns>
        public Player[] getPlayers() {
            return playersFileDAO.getPlayers();
        }

        /// <summary>
        /// Adds a new player to the database
        /// </summary>
        /// <param name="member"> The discord member that will gain a new acconut </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public Boolean addPlayer(DiscordMember member) {
            return playersFileDAO.addPlayer(member);
        } 

        /// <summary>
        /// Deletes a user's data based on the given ID
        /// </summary>
        /// <param name="UID"> The ID of the player to delete </param>
        /// <returns> A boolean indicating whether or not the action was successful </returns>
        public Boolean deletePlayer(ulong UID) {
            return playersFileDAO.deletePlayer(UID);
        }

        /// <summary>
        /// Communicator between front end and backend to retrieve 
        /// a page out of the list of seeds the player owns
        /// </summary>
        /// <param name="UID"> the id of the player </param>
        /// <param name="pageIndex"> the page to display </param>
        /// <returns> a tuple containing the page number displayed as well as a list of string
        ///             holding information on what is on said page </returns>
        public Tuple<int, List<String>> getSeeds(ulong UID, int pageIndex) {
            return playersFileDAO.getSeeds(UID, pageIndex);
        }

        /// <summary>
        /// Communicator between front end and backend to retrieve 
        /// a page out of the list of items the player owns
        /// </summary>
        /// <param name="UID"> the id of the player </param>
        /// <param name="pageIndex"> the page to display </param>
        /// <returns> a tuple containing the page number displayed as well as a list of string
        ///             holding information on what is on said page </returns>
        public Tuple<int, List<String>> getInventory(ulong UID, int pageIndex) {
            return playersFileDAO.getInventory(UID, pageIndex);
        }

        /// <summary>
        /// Communicator between front end and backend to retrieve 
        /// a page out of the list of pots the player owns
        /// </summary>
        /// <param name="UID"> the id of the player </param>
        /// <param name="pageIndex"> the page to display </param>
        /// <returns> a tuple containing the page number displayed as well as a list of string
        ///             holding information on what is on said page </returns>
        public Tuple<int, List<String>> getPots(ulong UID, int pageIndex) {
            return playersFileDAO.getPots(UID, pageIndex);
        }

        /// <summary>
        /// Communicator between front end and backend to retrieve 
        /// player info in order to display for the user
        /// </summary>
        /// <param name="UID"> the id of the player </param>
        /// <returns> a string containing information on the player </returns>
        public String getStatus(ulong UID) {
            return playersFileDAO.getStatus(UID);
        }
    }
}