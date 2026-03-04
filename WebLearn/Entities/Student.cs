namespace WebLearn.Entities;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

 
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}