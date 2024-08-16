using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmVille.FarmVille_api.src.Database.Utility;

namespace FarmVille.FarmVille_api.src.Database.Objects
{
    public class TableSchema
    {
        private int tableNumber;
        private String tableName;
        private List<AttributeSchema> attributes;
        private int numPages;
        private List<int> pageOrder;
        private int numRecords;
        
        public TableSchema(String tableName, int tableNumber) {
            this.tableName = tableName;
            this.tableNumber = tableNumber;
            this.attributes = new List<AttributeSchema>();
            this.numPages = 0;
            this.numRecords = 0;
            this.pageOrder = new List<int>();
        }

        public TableSchema(String tableName) {
            this.tableName = tableName;
            this.tableNumber = this.HashName();
            this.attributes = new List<AttributeSchema>();
            this.numPages = 0;
            this.numRecords = 0;
            this.pageOrder = new List<int>();
        }

        public int GetTableNumber() {
            return tableNumber;
        }

        public String GetTableName() {
            return tableName;
        }

        public void SetTableName(String name) {
            this.tableName = name;
            this.tableNumber = this.HashName();
        }

        public List<AttributeSchema> GetAttributes() {
            return attributes;
        }

        public void SetAttributes(List<AttributeSchema> attrs) {
            this.attributes = attrs;
        }

        public bool HasAttribute(String name) {
            foreach (AttributeSchema attr in this.attributes) {
            if (attr.GetAttributeName().equals(name)) {
                return true;
            }
            }
            return false;
        }

        public void AddAttribute(AttributeSchema attr) {
            this.attributes.Add(attr);
        }

        public int GetNumPages() {
            return numPages;
        }

        public int GetNumRecords() {
            return numRecords;
        }

        public void SetNumPages() {
            this.numPages = this.pageOrder.Count();
        }

        public int IncrementNumRecords() {
            this.numRecords += 1;
            return this.numRecords;
        }

        public int DecrementNumRecords() {
            this.numRecords -= 1;
            return this.numRecords;
        }

        public List<int> GetPageOrder() {
            return this.pageOrder;
        }

        public void SetPageOrder(List<int> pageOrder) {
            this.pageOrder = pageOrder;
        }

        public void AddPageNumber(int pageNumber) {
            this.pageOrder.Add(pageNumber);
            this.SetNumPages();
        }

        public void AddPageNumber(int numberBefore, int pageNumber) {
            int index = this.pageOrder.IndexOf(numberBefore);
            this.pageOrder.Insert(index + 1, pageNumber);
            this.SetNumPages();
        }

        private int HashName() {
            char[] chars = this.tableName.ToCharArray();
            int hash = 0;
            int index = 0;
            foreach (char c in chars) {
                hash += c.GetHashCode() + index;
                index++;
            }
            return hash;
        }

          public Datatype GetAttributeType(int index) {
            String type = this.attributes[index].GetDataType().toLowerCase();
            Datatype pkType = Datatype.NULL;
            switch (type) {
            case "integer":
                pkType = Datatype.INTEGER;
                break;
            case "double":
                pkType = Datatype.DOUBLE;
                break;
            case "boolean":
                pkType = Datatype.BOOLEAN;
                break;
            default:
                if (type.Contains("char")) {
                    pkType = Datatype.STRING;
                } else {
                    MessagePrinter.printMessage(MessageType.ERROR, String.format("Invalid data type: %s", type));
                }
                // should never reach the break because of the error message, just in case, test this
                break;
            }
            return pkType;
        }

        public int getPrimaryIndex() {
            // determine index of the primary key
            int primaryIndex = -1;
            for (int i = 0; i < this.attributes.Count(); i++) {
                if (this.attributes[i].IsPrimaryKey()) {
                    primaryIndex = i;
                }
            }
            return primaryIndex;
        }

        /**
        public void loadSchema(RandomAccessFile catalogAccessFile) throws Exception {
            // at this point both table name and table number have already been read

            // Read the pageNumber of the root of the B+ Tree
            this.indexRootNumber = catalogAccessFile.readInt();

            // Read the number of index pages from the catalog file
            this.numIndexPages = catalogAccessFile.readInt();

            // Read the number of pages from the catalog file
            this.numPages = catalogAccessFile.readInt();

            // Read page order from the catalog file
            for (int i = 0; i < this.numPages; i++) {
                this.pageOrder.add(catalogAccessFile.readInt());
            }

            // Read the number of records from the catalog file
            this.numRecords = catalogAccessFile.readInt();

            // Read the number of attributes from the catalog file
            int numOfAttributes = catalogAccessFile.readInt();

            // Iterate over each attribute and load its schema from the catalog file
            for (int i = 0; i < numOfAttributes; ++i) {
                // Create a new AttributeSchema instance
                AttributeSchema attributeSchema = new AttributeSchema();

                // Load attribute schema from the catalog file
                attributeSchema.loadSchema(catalogAccessFile);

                // Add the loaded attribute schema to the list of attributes
                this.attributes.add(attributeSchema);
            }
        }

        public void saveSchema(RandomAccessFile catalogAccessFile) throws Exception {
            // Write table name to the catalog file as UTF string
            catalogAccessFile.writeUTF(this.tableName);

            // Write table number to the catalog file
            catalogAccessFile.writeInt(this.tableNumber);

            // Write the pageNumber of the root of the B+ tree
            catalogAccessFile.writeInt(this.indexRootNumber);

            // Write number of index pages to the catalog file
            catalogAccessFile.writeInt(this.numIndexPages);

            // Write the number of pages to the catalog file
            catalogAccessFile.writeInt(this.numPages);

            // Write page order to the catalog file
            for (int i = 0; i < this.numPages; ++i) {
                catalogAccessFile.writeInt(this.pageOrder.get(i));
            }

            // Write the number of records to the catalog file
            catalogAccessFile.writeInt(this.numRecords);

            // Write the number of attributes to the catalog file
            catalogAccessFile.writeInt(this.attributes.size());

            // Iterate over each attribute and save its schema to the catalog file
            for (int i = 0; i < this.attributes.size(); ++i) {
                this.attributes.get(i).saveSchema(catalogAccessFile);
            }
        }
        */

    }
}