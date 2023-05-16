using ACMEIndustries.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACMEIndustries.Database
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public JsonDbContext Json { get; set; }

        private IHostingEnvironment _hostingEnvironment;
        public ApplicationDbContext(IHostingEnvironment env)
        {
            Json = GetContext();
            _hostingEnvironment = env;
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
