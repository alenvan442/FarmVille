using FarmVille_api.src.Main.Model.Structures.Items;
using FarmVille_api.src.Main.Model.Utilities;

namespace FarmVille_api.src.Main.Model.Persistence
{
    public class PlantsFileDAO
    {

        string plantsJson;
        JsonUtilities jsonUtilities;
        Dictionary<uint, Plant> plantsId;
        Dictionary<string, Plant> plantsName;

        /// <summary>
        /// Constructor for the DAO for plant items
        /// </summary>
        /// <param name="plantsJson"> String holding where the json file for the plants is </param>
        /// <param name="jsonUtilities"> Utilities to help with manipulating json files </param>
        public PlantsFileDAO(string plantsJson, JsonUtilities jsonUtilities) {

            this.plantsJson = plantsJson;
            this.jsonUtilities = jsonUtilities;
            this.plantsId = new Dictionary<uint, Plant>();
            this.plantsName = new Dictionary<string, Plant>();
            this.load();
        }

        /// <summary>
        /// Loads the plant json file into a more local inventory for
        /// ease of access throughout the program
        /// </summary>
        private void load() {
            Dictionary<string, Plant> tempPlants = jsonUtilities.JsonDeserializeAsync<Dictionary<string, Plant>>(plantsJson).Result;
            if (tempPlants != null)
            {
                foreach (Plant plant in tempPlants.Values)
                {
                    this.plantsId.Add(plant.id, plant);
                    this.plantsName.Add(plant.name, plant);
                }
            }

        }

        /// <summary>
        /// Gets a plant given an item's id
        /// </summary>
        /// <param name="id"> a ulong associated with a plant </param>
        /// <returns> The plant associated with the id, null if none was found </returns>
        public Plant getPlant(uint id) {
            Plant result;
            this.plantsId.TryGetValue(id, out result);
            return result;
        }

        /// <summary>
        /// Creates a clone of a plant with a different amount
        /// </summary>
        /// <param name="id"> The id of the plant to clone </param>
        /// <param name="amount"> The amount to set the clone to have </param>
        /// <returns> The newly cloned plant </returns>
        public Plant getPlantAmount(uint id, int amount = 1) {
            Plant originalPlant = this.getPlant(id);
            Plant newPlant = new Plant(originalPlant, amount);

            return newPlant;

        }

    }
}