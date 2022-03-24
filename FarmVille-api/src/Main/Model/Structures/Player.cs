using FarmVille_api.src.Main.Model.Structures.Items;

namespace FarmVille_api.src.Main.Model.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public class Player
    {

        public ulong UID { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Player() {
            //TODO
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Boolean plantSeed(Seeds seed) {
            //TODO
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="potIndex"></param>
        /// <returns></returns>
        public TimeSpan getPotTime(int potIndex) {
            //TODO
            return TimeSpan.Zero;
        }

    }
}