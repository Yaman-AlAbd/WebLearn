using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebLearn.Data;
using WebLearn.DTOs.Teacher;
using WebLearn.Entities;
using WebLearn.DTOs.Course;
using Microsoft.EntityFrameworkCore;

namespace WebLearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TeachersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TeacherResponseDto>>> GetAll()
    {
        var teachers = await _context.Teachers.ToListAsync();
        var response = _mapper.Map<List<TeacherResponseDto>>(teachers);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherResponseDto>> GetById(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher == null)
            return NotFound("Teacher not found");

        var response = _mapper.Map<TeacherResponseDto>(teacher);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TeacherResponseDto>> Create(CreateTeacherDto dto)
    {
        var teacher = _mapper.Map<Teacher>(dto);
        teacher.CreatedAt = DateTime.UtcNow;

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<TeacherResponseDto>(teacher);
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher == null)
            return NotFound("Teacher not found");

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // GET: api/teachers/1/courses
    [HttpGet("{id}/courses")]
    public async Task<ActionResult<List<CourseResponseDto>>> GetTeacherCourses(int id)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(t => t.Id == id);

        if (!teacherExists)
            return NotFound("Teacher not found");


        var courses = await _context.Courses
            .Where(c => c.TeacherId == id)
            .Include(c => c.Teacher)
            .ToListAsync();

        var response = _mapper.Map<List<CourseResponseDto>>(courses);
        return Ok(response);
    }

    // GET: api/teachers/{teacherId}/courses/{courseId}/students/count
    [HttpGet("{teacherId}/courses/{courseId}/students/count")]
    public async Task<ActionResult<CourseStudentCountDto>> GetCourseStudentCount(int teacherId, int courseId)
    {
        var course = await _context.Courses
            .Where(c => c.Id == courseId && c.TeacherId == teacherId)
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync();
        
        if (course == null)
            return NotFound("Course not found for this teacher");
        
        var response = _mapper.Map<CourseStudentCountDto>(course);
        return Ok(response);
    }
}