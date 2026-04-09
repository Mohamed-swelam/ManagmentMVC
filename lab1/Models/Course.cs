using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Course
    {
        [Key]
        public int crs_Id { get; set; }
        public string crs_Name { get; set; }
        public string Description { get; set; }
        public List<string> Topics { get; set; } = new List<string>();

        public int TotalDegree { get; set; }
        public int MinimumDegree { get; set; }
    }
}
