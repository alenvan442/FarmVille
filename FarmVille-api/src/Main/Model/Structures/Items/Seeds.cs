using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Items
{
    public class Seeds: Items
    {

        [JsonProperty("plantName")]
        public string plantName { get; private set; }
        [JsonProperty("yield")]
        public int yield { get; private set; }
        [JsonProperty("growthDuration")]
        public TimeSpan growDuration { get; private set; }

        /// <summary>
        /// Create a new item that is of type seed by giving it some passed in parameters
        /// This constructor will be used during json loading
        /// </summary>
        /// <param name="id"> The id of this seed </param> 
        /// <param name="amount"> The amount of this seed
        ///                       typically will only be 1 since json created seeds
        ///                       will be used as a template </param>
        /// <param name="buyPrice"> The price set on this seed to buy </param> 
        /// <param name="sellPrice"> The price set on this seed to sell </param>
        /// <param name="name"> The name of this seed </param> 
        /// <param name="plantName"> The name of the plant this seed grows </param> 
        /// <param name="yield"> The number of plants this seed will yield </param> 
        /// <param name="growthDuration"> How long it will take for the seed to fully grow </param> 
        [JsonConstructor]
        public Seeds(long id, int amount, double buyPrice, double sellPrice,
                        string name, string plantName, int yield, TimeSpan growthDuration): 
                        base(id, amount, buyPrice, sellPrice, name) {

            this.plantName = plantName;
            this.yield = yield;
            this.growDuration = growDuration;

        }

        /// <summary>
        /// Creating a new seed that is a clone of the passed in seed
        /// The clone will consist of a different amount
        /// </summary>
        /// 
        /// <param name="otherSeed"></param> The seed to use to clone
        /// <param name="amount"></param> The new amount of the seed, remember this seed is considered an item
        public Seeds(Seeds otherSeed, int amount): 
                        base(otherSeed.id, amount, otherSeed.buyPrice, otherSeed.sellPrice, otherSeed.name) {

            this.plantName = otherSeed.plantName;
            this.yield = otherSeed.yield;
            this.growDuration = otherSeed.growDuration;
        }

    }
}