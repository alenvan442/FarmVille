using System;
using System.Linq;
using System.Threading.Tasks;
using FarmVille.FarmVille_api.src.Database.Objects;
using FarmVille.FarmVille_api.src.Database.Utility;

namespace FarmVille.FarmVille_api.src.Database.StorageManager
{
    public class StorageManager
    {
        private PriorityQueue<Page> buffer;
        private int bufferSize;

        public StorageManager() {
            this.buffer = new PriorityQueue<Page>();
            this.bufferSize = DatabaseUtil.bufferSize;
        }


        public void get() {

        }

        public void insert() {
        
        }

        public void delete() {

        }

        public void update() {

        }

        //------------------------------------------I/O-----------------------------------------//
        private Page getPage(int pageNum) {
            foreach (Page i in this.buffer) {
                if (i.pageNum == pageNum) {
                    i.updatePriority();
                    return i;
                }
            }

            // page to search for is not currently in buffer, so we read from hardware
            this.readPageFromHardware(pageNum);
            // the needed page is now in the buffer at the end of the priority queue
            return this.buffer.GetLast();
        }

        private void readPageFromHardware(int pageNum) {

        }

        private void writePageToHardware(Page page) {

        }

        private void bufferFlush() {

        }


    }
}