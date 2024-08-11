using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille.FarmVille_api.src.Database.StorageManager
{
    public interface StorageManagerObjectInterface
    {
        public void writeToHardware(RandomAccessFile tableAccessFile) throws Exception;

        public void readFromHardware(RandomAccessFile tableAccessFile, TableSchema tableSchema) throws Exception;

        public int computeSize();
    }
}