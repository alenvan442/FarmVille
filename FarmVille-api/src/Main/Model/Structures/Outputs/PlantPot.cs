using FarmVille_api.src.Main.Model.Structures.Items;

namespace FarmVille_api.src.Main.Model.Structures.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public class PlantPot : Outputs
    {

        Seeds seed;

        public PlantPot(int yield, TimeSpan growthDuration, DateTime startingTime) : base(yield, growthDuration, startingTime)
        {
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
                base.setAttributes(seed.yield, seed.growDuration, DateTime.Now);
                return true;
            }
        }

    }
}