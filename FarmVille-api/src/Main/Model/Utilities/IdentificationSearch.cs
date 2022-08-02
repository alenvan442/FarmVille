using FarmVille_api.src.Main.Model.Persistence;
using FarmVille_api.src.Main.Model.Structures.Items;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public static class IdentificationSearch
    {

        static SeedsFileDAO? seedsFileDAO;
        static PlantsFileDAO? plantsFileDAO;

        public static void init(SeedsFileDAO seedsDAO, PlantsFileDAO plantsDAO) {
            seedsFileDAO = seedsDAO;
            plantsFileDAO = plantsDAO;
        }

        public static Item idSearch(Item item) {
            uint id = item.id;
            uint itemType = id >> 16;
            Item result;

            switch(itemType) {
                case 1:
                    result = seedSearch(id);
                    break;
                case 2:
                    result = plantSearch(id);
                    break;
                default:
                    return null;
            }

            result.amount = item.amount;
            return result;

        }

        private static Seeds seedSearch(uint id) {
            return seedsFileDAO.getSeeds(id);
        }

        private static Plant plantSearch(uint id) {
            return plantsFileDAO.getPlant(id);
        }

    }
}