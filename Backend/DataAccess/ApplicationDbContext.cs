using ACMEIndustries.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACMEIndustries.Database
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public JsonDbContext Json { get; set; }
        public ApplicationDbContext()
        {
            Json = GetContext();
        }
        public async Task SaveContextAsync()
        {
            var updatedJsonString = JsonConvert.SerializeObject(Json);
            await File.WriteAllTextAsync("Database.json", updatedJsonString);
        }

        private JsonDbContext GetContext()
        {
            using (StreamReader r = new StreamReader("Database.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<JsonDbContext>(json);
            }
        }
    }
}
