namespace lab1.Models
{
    public class Ins_Course
    {
        public int Hours { get; set; }
        public int ins_Id { get; set; }
        public Instructor? Instructor { get; set; }
        public int crs_Id { get; set; }
        public Course? Course { get; set; }
    }
}
