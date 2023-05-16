using ACMEIndustries.Models;

namespace ACMEIndustries.Database
{
    public class JsonDbContext
    {
        public List<User> Users { get; set; }

        public List<Role> Roles { get; set; }

        public List<Project> Projects { get; set; }

        public List<Gender> Genders { get; set; }
    }
}
