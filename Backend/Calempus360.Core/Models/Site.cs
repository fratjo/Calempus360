using Calempus360.Models.Models;
namespace Calempus360.Models.Models
{
    public class Site
    {
        public int Site_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int University_Id { get; set; }
        public University University { get; set; }
        public List<Site_Academic_Year> Sites_Academic_Year { get; set; }
        public List<Group> Groups { get; set; }
    }
}
