using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmVille_api.src.Database.StorageManager;

namespace FarmVille_api.src.Database.Objects
{
    public interface CatalogInterface
    {
        /**
    * The method to create a table in the database.
    *
    * @param tableSchema The schema of the table to be created.
    * @if an error occurs during the table creation process.
    */
    public void createTable(TableSchema tableSchema);

    /**
    * Method that deletes the schema for a table that is being dropped.
    * @param tableNumber - The old table # for the table we are updating due to being dropped.
    */
    public void dropTableSchema(int tableNumber);


    /**
    * Method that updates the Schema for a particular Table based on the result of an Alter command.
    * @param tableNumber - The # for the Table we are updating the schemea of.
    * @param op - the alter operation we performed on this table, ei: drop attr or add attr
    * @param attrName - Name of the Attr that is being altered in the table.
    * @param attrType - Type of the Attr that is being altered in the table.
    * @param val - Default value being added to the table if applicable.
    * @param notNull - The attribute cannot be null.
    * @param pKey - Whether or not the attribute is a primary key.
    * @param unique - Whether or not the attribute has to be unique.
    * @- Should only be thrown if the method is called in an incorrect fashion.
    * @return - If drop - The previous index of the dropped item. If add - the index of the new attr.
    */
    public void alterTableSchema(int tableNumber, String op, String attrName, String attrType, Object val, String isDeflt, bool notNull,
                                    bool pKey, bool unique);

    /**
    * Loads the catalog from hardware and obtains a random access file.
    *
    * @if an error occurs during loading the catalog
    */
    public void loadCatalog() ;

    /**
        * Saves the catalog to the storage.
        * The catalog information is saved to a file specified by the catalog location,
        * along with the schema information for each table.
        * @
        */
    public void saveCatalog() ;
        }
}