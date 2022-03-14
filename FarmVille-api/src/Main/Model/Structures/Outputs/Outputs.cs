using System.Text.Json.Serialization;

namespace FarmVille_api.src.Main.Model.Structures.Outputs
{
    public class Outputs
    {
        
        public int yield { get; private set; }
        public TimeSpan growthDuration { get; private set; }
        public DateTime startingTime { get; private set; }

        [JsonConstructor]
        public Outputs(int yield, TimeSpan growthDuration, DateTime startingTime) {
            this.yield = yield;
            this.growthDuration = growthDuration;
            this.startingTime = startingTime;
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

    }
}