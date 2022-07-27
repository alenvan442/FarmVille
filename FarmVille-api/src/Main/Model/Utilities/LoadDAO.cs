using DSharpPlus.Entities;
using FarmVille.Commands;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Persistence;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public static class LoadDAO
    {

        static PlantPotsFileDAO plantPotsFileDAO;
        static PlayersFileDAO playersFileDAO;
        static SeedsFileDAO seedsFileDAO;
        static ShopFileDAO shopFileDAO;
        static PlantPotController plantPotController;
        static PlayerController playerController;
        static ShopController shopController;
        static Farming farmingCommands;
        static Menu menuCommands;

        /// <summary>
        /// Loads the FileDAOs
        /// </summary>
        public static void load() {
            JsonUtilities json = new JsonUtilities();
            EmbedUtilities embedUtilities = new EmbedUtilities();
            playersFileDAO = new PlayersFileDAO(StaticUtil.playersJson, json, seedsFileDAO);
            seedsFileDAO = new SeedsFileDAO(StaticUtil.seedsJson, json);
            shopFileDAO = new ShopFileDAO(StaticUtil.shopJson, json);
            plantPotsFileDAO = new PlantPotsFileDAO(playersFileDAO, seedsFileDAO);

            plantPotController = new PlantPotController(plantPotsFileDAO);
            playerController = new PlayerController(playersFileDAO);
            shopController = new ShopController(shopFileDAO);

            farmingCommands = new Farming(plantPotController, playerController, embedUtilities);
            menuCommands = new Menu(playerController, embedUtilities);

        }

        /// <summary>
        /// Adds every player in a discord guild to the database
        /// Creating an empty, or basic player account
        /// </summary>
        /// <param name="guild"> The guild that was recently connected to </param>
        public async static void addPlayers(DiscordGuild guild) {
            foreach(DiscordMember i in await guild.GetAllMembersAsync()) {
                playersFileDAO.addPlayer(i);
            }
        } 
    }
}