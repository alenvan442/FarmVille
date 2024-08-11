using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmVille_api.src.Database.StorageManager;
using static FarmVille_api.src.Database.Objects.MessagePrinter;

namespace FarmVille_api.src.Database.Objects
{
    public class Page : BufferPage
    {
        private int numRecords;
        private List<Record> records;

        public Page(int numRecords, int tableNumber, int pageNumber) : base(tableNumber, pageNumber) {
            this.numRecords = numRecords;
            this.changed = false;
            this.records = new List<Record>();
        }

        public Page() : base(0,0) {
        }

        public int getNumRecords() {
            return numRecords;
        }

        public void setNumRecords() {
            this.numRecords = this.records.Count;
        }

        public List<Record> getRecords() {
            return records;
        }

        public void setRecords(List<Record> records) {
            this.records = records;
            this.setNumRecords();
        }

        public int getRecordLocation(Record record, int primaryKeyIndex) {
            for (int i = 0; i < this.records.Count; i++) {
                if (record.compareTo(this.records[i], primaryKeyIndex) == 0) {
                    return i;
                }
            }
            // error 404
            MessagePrinter.printMessage(MessageType.ERROR, "Unable to find record in page: getRecordLocation");
            return -1;
        }

        /**
        * Adds a record to the page in the correct order
        *
        * @param record    The record to be inserted
        *
        * @return          true: insert success
        *                  false: page is full
        * @throws Exception
        */
        public bool addNewRecord(Record record) {
            Catalog catalog = Catalog.getCatalog();
            // check if record can fit in this page.
            if ((catalog.getPageSize() - this.computeSize()) < record.computeSize()) {
                return false;
            } else {
                Dictionary<Int32, TableSchema> schemas = catalog.getSchemas();
                TableSchema schema = schemas[this.tableNumber];
                int primaryIndex = schema.getPrimaryIndex();
                String primaryType = schema.getAttributes().get(primaryIndex).getDataType();
                Comparison<Record> comparator = recordComparator(primaryIndex, primaryType);
                // Add record
                this.records.Add(record);
                // sort
                this.records.Sort(comparator);

                this.setNumRecords();
                this.changed = true;
                this.setPriority();
                return true;
            }
        }

        /**
        * Adds a record to the page at a specific index
        * @param record    The record to be inserted
        * @param index     The index to insert at
        * @return          true: insert success
        *                  false: page is full
        * @throws Exception
        */
        public bool addNewRecord(Record record, int index) {
            if ((Integer)record.getValues().get(0) == 150 ||
            (Integer)record.getValues().get(0) == 156) {
                System.Console.WriteLine("notify");
            }
            Catalog catalog = Catalog.getCatalog();
            // check if record can fit in this page.
            if ((catalog.getPageSize() - this.computeSize()) < record.computeSize()) {
                return false;
            } else {
                this.records.add(index, record);
                this.setNumRecords();
                this.changed = true;
                this.setPriority();
                return true;
            }
        }

        /**
        * Deletes a record at a specific index
        *
        * @param index     The index to delete the record at
        *
        * @return          The deleted record
        */
        public Record deleteRecord(int index) {
            Record removed = this.records.remove(index);
            this.changed = true;
            this.setPriority();
            this.setNumRecords();
            System.out.println("Deleted record pk: " + removed.getValues().get(0));
            return removed;
        }

        /**
        * Replaces a record at a given index
        *
        * @param index     The index to replace the record at
        * @param record    The record to replace with
        *
        * @return          bool, indicating success status
        */
        public bool updateRecord(int index, Record record) {
            Record curr = this.getRecords()[index];
            if (curr.Equals(null)) {
                return false;
            } else {
                this.getRecords().Insert(index, record);
                this.changed = true;
                this.setPriority();
                return true;
            }
        }

        /**
        * returns the number of bytes of space is left in this page
        *
        * @return  int - number of bytes of space left
        * @throws Exception
        */
        public int computeSize() {
            // Page: numRecord, PageNumber, records..
            /*
            int size = Integer.BYTES * 2;
            for (Record record: this.records) {
                size += record.computeSize();
            }
            return size;
            */
            return 0;
        }

        public void writeToHardware(RandomAccessFile tableAccessFile) {
            /*
            tableAccessFile.writeInt(this.numRecords);
            tableAccessFile.writeInt(this.pageNumber);
            for (Record record: this.records) {
            record.writeToHardware(tableAccessFile);
            }
            */
        }

        public void readFromHardware(RandomAccessFile tableAccessFile, TableSchema tableSchema) {
            /*
            for (int i=0; i < numRecords; ++i) {
                Record record = new Record(new ArrayList<>());
                record.readFromHardware(tableAccessFile, tableSchema);
                this.records.add(record);
            }
            */
        }

        /**
        * Creates a comparator to compare records based on the specified primary key index and data type.
        *
        * @param primaryKeyIndex The index of the primary key in each record.
        * @param dataType        The data type to determine the comparison method. Supported types are "integer", "double",
        *                        "bool", "char(x)", and "varchar(x)" where x represents the length.
        * @return A comparator for comparing records based on the specified primary key index and data type.
        * @throws IllegalArgumentException If an unsupported data type is provided.
        */
        private Comparison<Record> recordComparator(int primaryKeyIndex, String dataType) {
            /*return (record1, record2) -> {
                Object obj1 = record1.getValues().get(primaryKeyIndex);
                Object obj2 = record2.getValues().get(primaryKeyIndex);

                if (dataType.equalsIgnoreCase("integer")) {
                    Integer int1 = (Integer) obj1;
                    Integer int2 = (Integer) obj2;
                    return int1.compareTo(int2);
                } else if (dataType.equalsIgnoreCase("double")) {
                    Double double1 = (Double) obj1;
                    Double double2 = (Double) obj2;
                    return double1.compareTo(double2);
                } else if (dataType.equalsIgnoreCase("bool")) {
                    bool bool1 = (bool) obj1;
                    bool bool2 = (bool) obj2;
                    return bool.compare(bool1, bool2);
                } else if (dataType.contains("char") || dataType.contains("varchar")) {
                    String str1 = obj1.toString();
                    String str2 = obj2.toString();
                    return str1.compareTo(str2);
                } else {
                    throw new IllegalArgumentException("Unsupported data type: " + dataType);
                }
            };
            */
            return null;
        }

        public override bool Equals(Object obj) {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            Page other = (Page) obj;
            if (pageNumber != other.pageNumber)
                return false;
            return true;
        }
    }
}