using System.Text.Json.Serialization;
using FarmVille_api.src.Main.Model.Structures.Items;
using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Structures.Outputs
{
    /// <summary>
    /// 
    /// </summary>
    public class Output
    {
        [JsonProperty("Yield")]
        public int yield { get; private set; }
        [JsonProperty("GrowthDuration")]
        public TimeSpan growthDuration { get; private set; }
        [JsonProperty("StartingTime")]
        public DateTime startingTime { get; private set; }
        [JsonProperty("ID")]
        public uint id { get; private set; }
        [JsonProperty("OutputID")]
        public uint outputID { get; private set; }

        /// <summary>
        /// Constructor of the output data class
        /// </summary>
        /// <param name="yield"> The amount of items that will be given upon harvesting </param>
        /// <param name="growthDuration"> The time it takes before able to harvest </param>
        /// <param name="startingTime"> The time that the growth duration started counting down from </param>
        /// <param name="id"> The id of this output object </param>
        /// <param name="outputID"> The id of the item obtained upon harvesting </param>
        [Newtonsoft.Json.JsonConstructor]
        public Output(int yield, TimeSpan growthDuration, DateTime startingTime, uint id, uint outputID) {
            this.yield = yield;
            this.growthDuration = growthDuration;
            this.startingTime = startingTime;
            this.id = id;
            this.outputID = outputID;
        }

        /// <summary>
        /// Find the remaining time before the output is ready to be gathered
        /// </summary>
        /// <returns> A time span indicating how much time is
        ///        left until the output is ready to be gathered </returns>
        public TimeSpan remainingTime() {

            //if starting time has not been set yet, meaning this output is empty
            //then return a time span with -1 ticks
            if(startingTime.Equals(DateTime.MinValue)) {
                return new TimeSpan(-1);
            } else {

                //if the output is not empty then get the current time
                DateTime currentTime = DateTime.Now;

                //find how much time is left on the growth duration
                TimeSpan elapsedTime = currentTime - startingTime;
                TimeSpan timeLeft = growthDuration - elapsedTime;

                //analyze the time left and return it
                if(timeLeft <= TimeSpan.Zero) {
                    timeLeft = TimeSpan.Zero;
                }

                return timeLeft.Duration();

            }
        }

        /// <summary>
        /// Sets the attributes of the member of this output
        /// </summary>
        /// 
        /// <param name="yield"> How much output is produced upon harvest </param>
        /// <param name="growthDuration"> how long it takes until player can harvest </param>
        /// <param name="startingTime"> when did the member first start growing? </param>
        public void setAttributes(int yield, TimeSpan growthDuration, DateTime startingTime, uint outputID) {
            this.yield = yield;
            this.growthDuration = growthDuration;
            this.startingTime = startingTime;
            this.outputID = outputID;
        }

        /// <summary>
        /// The action of harvesting from this plant pot
        /// We reset the time the input was placed into the output container
        /// We reset the time it takes for the input to become harvestable
        /// Create a new "empty" item which consists of only the outputted item's
        /// id and amount
        /// </summary>
        /// <returns> The created "empty" item that was harvested </returns>
        public Item harvest() {

            this.startingTime = DateTime.MinValue;
            this.growthDuration = TimeSpan.Zero;

            Item newItem = new Item(this.outputID, 0, 0, "", this.yield);
            this.outputID = 0;
            this.yield = 0;

            return newItem;
        }

        /// <summary>
        /// The to string for an output
        /// returns the string containing the remaining time of this output
        /// </summary>
        /// <returns> a string representing this output containiner </returns>
        public override string ToString()
        {
            return this.remainingTime().ToString();
        }

    }
}