using FarmVille_api.src.Main.Model.Structures.Items;
using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public class PlantPot : Output
    {
        [JsonProperty("Seed")]
        Seeds? seed;

        /// <summary>
        /// Constructor for an empty plant pot
        /// </summary>
        /// <param name="id"> The id of this plant pot </param>
        /// <returns></returns>
        public PlantPot(Seeds Seed = null) : base(0, TimeSpan.Zero, DateTime.MinValue, 0, 196609) {
            this.seed = Seed;

        }

        /// <summary>
        /// Returns a boolean indicating whether or not this plant pot is empty.
        /// </summary>
        /// 
        /// <returns> 
        /// true is this plant pot is currently empty with no seed growing, 
        /// false if otherwise 
        /// </returns>
        public Boolean isEmpty() {
            return (this.seed == null);
        } 

        /// <summary>
        /// Harvests the crop and resets the plant pot for further planting another seed
        /// </summary>
        /// <returns> The item that was harvested </returns>
        public Item harvest() {

            if (!this.seed.regrow)
            {
                this.seed = null;
            }

            return base.output(this.seed.regrow);
        } 

        /// <summary>
        /// Plant a seed in this specific plant pot
        /// </summary>
        /// 
        /// <param name="seed"> the to be planted seed </param>
        /// 
        /// <returns>
        /// true if a successful plant happened
        /// false if a problem occured, the plant pot is already occupied
        /// </returns>
        public Boolean plantSeed(Seeds seed) {

            //return false if there is already a seed planted in this
            if(!this.isEmpty()) {
                return false;
            } else {
                //if this plant pot is not occupied then plant the seed and set the attributes
                //return true to indicate a successful plant
                this.seed = seed;
                base.setAttributes(seed.yield, seed.growDuration, DateTime.Now, seed.plantID);
                return true;
            }
        }

        /// <summary>
        /// clears itself
        /// </summary>
        public void clear() {
            this.seed = null;
            base.clear();
        }

        /// <summary>
        /// ToString for a plant pot
        /// Line 1: The identifier for the pot
        /// Line 2: What the pot is growing, display empty if so
        /// Line 3: The remaining time, not showned if the pot is empty
        /// </summary>
        /// <returns> a string displaying all information in regards to this plant pot </returns>
        public string ToString(int index) { 
            
            string result;
            if(this.isEmpty()) {
                result = "Plant Pot: #" + index +
                                 "\nPlant: Empty\n\n";
            } else
            {
                result = "Plant Pot: #" + index +
                                "\nPlant: " + this.seed.plantName +
                                "\n\nTime Remaining: " + base.ToString();
            }
            return result;
        }

    }
}