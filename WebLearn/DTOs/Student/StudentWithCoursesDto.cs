namespace WebLearn.DTOs.Student;

public class StudentWithCoursesDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int CoursesCount { get; set; } 
    public List<StudentCourseDto> Courses { get; set; } = new();
}
public class StudentCourseDto
{
    //public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
}