namespace WebLearn.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxStudents { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

   
    public int TeacherId { get; set; }

  
    public Teacher Teacher { get; set; } = null!;

  
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();  
}