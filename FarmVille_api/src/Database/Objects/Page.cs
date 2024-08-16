using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille.FarmVille_api.src.Database.Objects
{
    public class Page : IComparable<Page>
    {
        public long value;
        public int pageNum;

        public Page(int pageNum) {
            this.pageNum = pageNum;
            this.updatePriority();
        }

        public int CompareTo(Page? other)
        {
            if (other == null) {
                return -1;
            } else if (other.GetType() == typeof(Page)) {
                long otherValue = other.value;
                if (this.value < otherValue) {
                    return -1;
                } else if (this.value > otherValue) {
                    return 1; // value is the date, so we want the LRU to be the least in date and to have the highest priority
                } else {
                    return 0;
                }
            } else {
                return -1;
            }
        }

        public void updatePriority() {
            this.value = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}