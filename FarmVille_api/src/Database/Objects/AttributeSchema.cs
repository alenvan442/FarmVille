using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille_api.src.Database.Objects
{
    public class AttributeSchema
    {
       private String attributeName;
        private String dataType;
        private bool notNull;
        private bool primaryKey;
        private bool unique;

        public AttributeSchema(String attributeName, String dataType, bool notNull, bool primaryKey, bool unique) {
            this.attributeName = attributeName;
            this.dataType = dataType;
            this.notNull = notNull;
            this.primaryKey = primaryKey;
            this.unique = unique;
        }

        public AttributeSchema(String attributeName, AttributeSchema oldSchema) {
            this.attributeName = attributeName;
            this.dataType = oldSchema.getDataType();
            this.notNull = isNotNull();
            this.primaryKey = oldSchema.isPrimaryKey();
            this.unique = oldSchema.isUnique();
        }

        public String getAttributeName() {
            return attributeName;
        }

        public void setAttributeName(String attributeName) {
            this.attributeName = attributeName;
        }

        public String getDataType() {
            return dataType;
        }

        public void setDataType(String dataType) {
            this.dataType = dataType;
        }

        public bool isNotNull() {
            return notNull;
        }

        public void setNotNull(bool notNull) {
            this.notNull = notNull;
        }

        public bool isPrimaryKey() {
            return primaryKey;
        }

        public void setPrimaryKey(bool primaryKey) {
            this.primaryKey = primaryKey;
        }

        public bool isUnique() {
            return unique;
        }

        public void setUnique(bool unique) {
            this.unique = unique;
        }

        /**
        * {@inheritDoc}
        
        @Override
        public void saveSchema(RandomAccessFile catalogAccessFile) throws IOException {
            // Write attribute name to the catalog file as UTF string
            catalogAccessFile.writeUTF(this.attributeName);

            // Write data type to the catalog file as UTF string
            catalogAccessFile.writeUTF(this.dataType);

            // Write whether the attribute is not null to the catalog file
            catalogAccessFile.writebool(this.notNull);

            // Write whether the attribute is a primary key to the catalog file
            catalogAccessFile.writebool(this.primaryKey);

            // Write whether the attribute is unique to the catalog file
            catalogAccessFile.writebool(this.unique);
        }

        @Override
        public void loadSchema(RandomAccessFile catalogAccessFile) throws Exception {
            // Read attribute Name
            this.attributeName = catalogAccessFile.readUTF();

            // Read datatype
            this.dataType = catalogAccessFile.readUTF();

            // Read whether the attribute is not null from the catalog file
            this.notNull = catalogAccessFile.readbool();

            // Read whether the attribute is a primary key from the catalog file
            this.primaryKey = catalogAccessFile.readbool();

            // Read whether the attribute is unique from the catalog file
            this.unique = catalogAccessFile.readbool();
        } 
        */
    }
}