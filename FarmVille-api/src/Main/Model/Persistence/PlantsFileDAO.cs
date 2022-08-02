using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class PlantsFileDAO
    {

        string plantsJson;
        JsonUtilities jsonUtilities;
        Dictionary<uint, Plant> plantsId;

        public PlantsFileDAO(string plantsJson, JsonUtilities jsonUtilities) {

            this.plantsJson = plantsJson;
            this.jsonUtilities = jsonUtilities;
            this.plantsId = new Dictionary<uint, Plant>();
            this.load();
        }

        private void load() {
            List<Plant> tempPlants = jsonUtilities.JsonDeserializeAsync<List<Plant>>(plantsJson).Result;

            foreach(Plant plant in tempPlants) {
                this.plantsId.Add(plant.id, plant);
            }

        }

        public Plant getPlant(uint id) {
            Plant result;
            this.plantsId.TryGetValue(id, out result);
            return result;
        }

        public Plant getPlantAmount(uint id, int amount) {
            Plant originalPlant = this.getPlant(id);
            Plant newPlant = new Plant(originalPlant, amount);

            return newPlant;

        }

    }
}