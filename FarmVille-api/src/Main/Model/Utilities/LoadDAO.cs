using DSharpPlus.Entities;
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

        /// <summary>
        /// Loads the FileDAOs
        /// </summary>
        public static void load() {
            JsonUtilities json = new JsonUtilities();
            playersFileDAO = new PlayersFileDAO(StaticUtil.playersJson, json, seedsFileDAO);
            seedsFileDAO = new SeedsFileDAO(StaticUtil.seedsJson, json);
            shopFileDAO = new ShopFileDAO(StaticUtil.shopJson, json);
            plantPotsFileDAO = new PlantPotsFileDAO(playersFileDAO, seedsFileDAO);

            plantPotController = new PlantPotController(plantPotsFileDAO);
            playerController = new PlayerController(playersFileDAO);
            shopController = new ShopController(shopFileDAO);

        }

        public async static void addPlayers(DiscordGuild guild) {
            foreach(DiscordMember i in await guild.GetAllMembersAsync()) {
                playersFileDAO.addPlayer(i);
            }
        } 
    }
}