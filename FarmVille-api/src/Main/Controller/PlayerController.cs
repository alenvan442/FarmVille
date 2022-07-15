using DSharpPlus.Entities;
using FarmVille_api.src.Main.Model.Persistence;
using FarmVille_api.src.Main.Model.Structures;

namespace FarmVille_api.src.Main.Controller
{
    /// <summary>
    /// This controller will receive information from the view and either add, delete, or update a player accordingly
    /// The main purpose of this controller is to receive and delegate tasks that involves the creation or deletion of a player
    /// </summary>
    public class PlayerController
    {
        PlayersFileDAO playersFileDAO;
        public PlayerController(PlayersFileDAO playersFileDAO)
        {
            this.playersFileDAO = playersFileDAO;
        }

        public Player getPlayer(ulong UID) {
            return playersFileDAO.getPlayer(UID);
        }

        public Player[] getPlayers() {
            return playersFileDAO.getPlayers();
        }

        public Boolean addPlayer(DiscordMember member) {
            return playersFileDAO.addPlayer(member);
        } 

        public Boolean deletePlayer(ulong UID) {
            return playersFileDAO.deletePlayer(UID);
        }
    }
}