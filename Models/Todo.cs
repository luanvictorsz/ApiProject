namespace ApiProject.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Done { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
     
    }
}