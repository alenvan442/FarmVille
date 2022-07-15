using System.Text.Json.Serialization;
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
        public int id { get; private set; }
        [JsonProperty("OutputID")]
        public long outputID { get; private set; }

        [Newtonsoft.Json.JsonConstructor]
        public Output(int yield, TimeSpan growthDuration, DateTime startingTime, int id, long outputID) {
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
        public void setAttributes(int yield, TimeSpan growthDuration, DateTime startingTime, long outputID) {
            this.yield = yield;
            this.growthDuration = growthDuration;
            this.startingTime = startingTime;
            this.outputID = outputID;
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