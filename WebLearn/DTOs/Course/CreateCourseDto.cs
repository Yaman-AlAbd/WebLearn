namespace WebLearn.DTOs.Course;

public class CreateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxStudents { get; set; }
    public DateTime StartDate { get; set; }
    public int TeacherId { get; set; } 
}