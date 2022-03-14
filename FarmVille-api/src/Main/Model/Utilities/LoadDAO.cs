using FarmVille_api.src.Main.Model.Persistence;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public static class LoadDAO
    {

        static PlantPotsFileDAO plantPotsFileDAO;
        static PlayersFileDAO playersFileDAO;
        static SeedsFileDAO seedsFileDAO;
        static ShopFileDAO shopFileDAO;

        /// <summary>
        /// Loads the FileDAOs
        /// </summary>
        public static void load() {
            JsonUtilities json = new JsonUtilities();
            playersFileDAO = new PlayersFileDAO(StaticUtil.playersJson, json);
            plantPotsFileDAO = new PlantPotsFileDAO(playersFileDAO);
            seedsFileDAO = new SeedsFileDAO(StaticUtil.seedsJson, json);
            shopFileDAO = new ShopFileDAO(StaticUtil.shopJson, json);
        }

    }
}