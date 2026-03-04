using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebLearn.Data;
using WebLearn.DTOs.Course;
using WebLearn.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebLearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CoursesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<ActionResult<List<CourseResponseDto>>> GetAll()
    {
        var courses = await _context.Courses
            .Include(c => c.Teacher)
            .ToListAsync();

        var response = _mapper.Map<List<CourseResponseDto>>(courses);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseResponseDto>> GetById(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return NotFound("Course not found");

        var response = _mapper.Map<CourseResponseDto>(course);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CourseResponseDto>> Create(CreateCourseDto dto)
    {
        var teacherExists = await _context.Teachers
            .AnyAsync(t => t.Id == dto.TeacherId);

        if (!teacherExists)
            return BadRequest("Teacher not found");

        var course = _mapper.Map<Course>(dto);
        course.CreatedAt = DateTime.UtcNow;

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        await _context.Entry(course)
            .Reference(c => c.Teacher)
            .LoadAsync();

        var response = _mapper.Map<CourseResponseDto>(course);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CourseResponseDto>> Update(int id, UpdateCourseDto dto)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return NotFound("Course not found");


        var teacherExists = await _context.Teachers
            .AnyAsync(t => t.Id == dto.TeacherId);

        if (!teacherExists)
            return BadRequest("Teacher not found");

        _mapper.Map(dto, course);
        await _context.SaveChangesAsync();


        await _context.Entry(course)
            .Reference(c => c.Teacher)
            .LoadAsync();

        var response = _mapper.Map<CourseResponseDto>(course);
        return Ok(response);
    }

    // DELETE: api/courses/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
            return NotFound("Course not found");


        var hasEnrollments = await _context.Enrollments
            .AnyAsync(e => e.CourseId == id);

        if (hasEnrollments)
            return BadRequest("Cannot delete course that has enrolled students");

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}