using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using System.Text.Json;

namespace ElectroQuest.Infrastructure.Analytics.Adapters
{
    public class ReadLocal : IReadLocal
    {
        // this a prototype implementation 
        // we assume that the data (sample data) will fit into the memory
        // in real production we have to stream chunk based .
        // path we be passed from the appsettings.json .
        public async Task<TResult?> ReadLocalAsync<TResult>(string path, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("type : cannot be null or empty");
            }
            switch(type.ToLower().TrimStart('.'))
            {
                case "json":
                return await ReadJson<TResult>(path);
                default:
                throw new Exception($"Not Supported File Extension : {type.ToLower().TrimStart('.')}");
            }
        }
        async Task<TResult?> ReadJson<TResult>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Path Cannot Be Empty");
            }
            string extension = Path.GetExtension(path);
            if(extension != ".json")
            {
                throw new Exception($"Configured extension : json \n Given : {extension}");
            }
            if (!File.Exists(path))
            {
                throw new Exception("File Not Found");
            }
            using (FileStream fs = File.OpenRead(path))
            using (StreamReader reader = new StreamReader(fs))
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                return await JsonSerializer.DeserializeAsync<TResult>(reader.BaseStream , options);
            }
        }
        // ReadXml() , ReadTXT , Read.....
    }
}
