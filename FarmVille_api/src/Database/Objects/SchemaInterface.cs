using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille_api.src.Database.Objects
{
    public interface SchemaInterface
    {
        /**
        * Loads schema information from the specified random access file.
        *
        * @param catalogAccessFile the random access file from which the schema information will be loaded
        * @throws Exception if an I/O error occurs while writing to the random access file
        */
        public void loadSchema(RandomAccessFile catalogAccessFile) throws Exception;

        /**
        * Saves the schema information to the specified random access file.
        *
        * @param catalogAccessFile the random access file where the table schema information will be saved
        * @throws Exception if an I/O error occurs while writing to the random access file
        */
        public void saveSchema(RandomAccessFile catalogAccessFile) throws Exception; 
    }
}