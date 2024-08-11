using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille_api.src.Database.Objects
{
    public class Table
    {
        private int numPages;
        private List<Page> pages;
        private String name;

        public Table(int numPages, List<Page> pages) {
            this.numPages = numPages;
            this.pages = pages;
        }

        public int getNumPages() {
            return numPages;
        }

        public String getName() {
            return this.name;
        }

        public void addPage(Page page) {
            this.pages.add(page);
        }

        public void setNumPages(int numPages) {
            this.numPages = numPages;
        }

        public List<Page> getPages() {
            return pages;
        }

        public void setPages(List<Page> pages) {
            this.pages = pages;
        }
    }
}