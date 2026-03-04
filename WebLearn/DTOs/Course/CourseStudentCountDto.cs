namespace WebLearn.DTOs.Course;

public class CourseStudentCountDto
{
    public int TeacherId { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;

    public int StudentCount { get; set; }
}