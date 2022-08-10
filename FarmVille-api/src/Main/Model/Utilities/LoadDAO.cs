using DSharpPlus.Entities;
using FarmVille.Commands;
using FarmVille_api.src.Main.Controller;
using FarmVille_api.src.Main.Model.Persistence;
using FarmVille_api.src.Main.View.Discord.Commands;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public static class LoadDAO
    {

        static PlantPotsFileDAO? plantPotsFileDAO;
        static PlayersFileDAO? playersFileDAO;
        static SeedsFileDAO? seedsFileDAO;
        static ShopFileDAO? shopFileDAO;
        static PlantPotController? plantPotController;
        static PlayerController? playerController;
        static ShopController? shopController;
        static Farming? farmingCommands;
        static Menu? menuCommands;
        static PlantsFileDAO? plantsFileDAO;

        /// <summary>
        /// Loads the FileDAOs
        /// </summary>
        public static void load() {
            JsonUtilities json = new JsonUtilities();
            seedsFileDAO = new SeedsFileDAO(StaticUtil.seedsJson, json);
            playersFileDAO = new PlayersFileDAO(StaticUtil.playersJson, json, seedsFileDAO);
            shopFileDAO = new ShopFileDAO(StaticUtil.shopJson, json);
            plantPotsFileDAO = new PlantPotsFileDAO(playersFileDAO, seedsFileDAO);
            plantsFileDAO = new PlantsFileDAO(StaticUtil.plantsJson, json);

            plantPotController = new PlantPotController(plantPotsFileDAO, playersFileDAO);
            playerController = new PlayerController(playersFileDAO);
            shopController = new ShopController(shopFileDAO, playersFileDAO, plantsFileDAO);

            CommandsHelper.setup(plantPotController, playerController, shopController);
            

            IdentificationSearch.init(seedsFileDAO, plantsFileDAO);

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