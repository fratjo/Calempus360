namespace Calempus360.Models.Models
{
    public class University
    {
        public int University_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Site> Sites { get; set; }
    }
}
