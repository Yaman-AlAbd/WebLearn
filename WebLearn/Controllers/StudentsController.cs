using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLearn.Data;
using WebLearn.DTOs.Student;
using WebLearn.Entities;
using AutoMapper;
namespace WebLearn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    
    public StudentsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<StudentResponseDto>>> GetAll()
    {
        var students = await _context.Students.ToListAsync();

       
        var response = _mapper.Map<List<StudentResponseDto>>(students);

        return Ok(response);
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentResponseDto>> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound("Student not found");

       
        var response = _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create(CreateStudentDto dto)
    {
        
        
        var student = _mapper.Map<Student>(dto);
        student.CreatedAt = DateTime.UtcNow;

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<StudentResponseDto>(student);

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, response);
    }

    // PUT: api/students/1
    [HttpPut("{id}")]
    public async Task<ActionResult<StudentResponseDto>> Update(int id, UpdateStudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound("Student not found");
        
        _mapper.Map(dto, student);

        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<StudentResponseDto>(student);

        return Ok(response);
    }

    // DELETE: api/students/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound("Student not found");
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

// GET: api/students/search?query=yam
    [HttpGet("search")]
    public async Task<ActionResult<List<StudentResponseDto>>> Search([FromQuery] string query)
    {
        
       
        
        var students = await _context.Students
            
            .Where(s =>
                EF.Functions.ILike(s.FullName, $"%{query.Trim()}%") )
            .OrderBy(s => s.FullName)
            .ToListAsync();

       
        var response = _mapper.Map<List<StudentResponseDto>>(students);

        return Ok(response);
    }
    
    [HttpGet("with-courses")]
    public async Task<ActionResult<List<StudentWithCoursesDto>>> GetStudentsWithCourses()
    {
        var students = await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .ThenInclude(c => c.Teacher)
            .OrderBy(s => s.FullName)
            .ToListAsync();

        var response = _mapper.Map<List<StudentWithCoursesDto>>(students);
        return Ok(response);
    }
}
