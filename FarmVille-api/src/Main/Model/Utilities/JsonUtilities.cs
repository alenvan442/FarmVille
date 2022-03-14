using System.Text;
using Newtonsoft.Json;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public class JsonUtilities
    {

        /*
         * Generic method that deserializes a file into the given object
         * 
         * @param filename: the string of the file's path
         * 
         * @return: the object created after deserializing
         */
        public async Task<T> JsonDeserializeAsync<T>(string filename)
        {
            var json = string.Empty;

            //open the file to read
            using (var fs = File.OpenRead(filename))

            //read the file
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /*
         * Generic method that serializes an object into a json file
         * 
         * @param obj: the obj to be serialized
         * @param filename: the path of the file to be written to
         */
        public void JsonSerialize<T>(T obj, string filename)
        {
            //serialize the obj into a json formatted string
            string jsonStr = JsonConvert.SerializeObject(obj, formatting: Formatting.Indented);

            //write the json string into a file overwritting any previous data in it
            File.WriteAllText(filename, jsonStr);

        }

    }
}