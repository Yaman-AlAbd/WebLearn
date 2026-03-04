using AutoMapper;
using WebLearn.Data;
using WebLearn.DTOs.Enrollment;
using WebLearn.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebLearn.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EnrollmentsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/enrollments
    [HttpGet]
    public async Task<ActionResult<List<EnrollmentResponseDto>>> GetAll()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();

        var response = _mapper.Map<List<EnrollmentResponseDto>>(enrollments);
        return Ok(response);
    }

    // GET: api/enrollments/1
    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentResponseDto>> GetById(int id)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrollment == null)
            return NotFound("Enrollment not found");

        var response = _mapper.Map<EnrollmentResponseDto>(enrollment);
        return Ok(response);
    }

    // POST: api/enrollments
    [HttpPost]
    public async Task<ActionResult<EnrollmentResponseDto>> Create(CreateEnrollmentDto dto)
    {
        // تحقق: الطالب موجود؟
        var studentExists = await _context.Students
            .AnyAsync(s => s.Id == dto.StudentId);

        if (!studentExists)
            return BadRequest("Student not found");

        // تحقق: الكورس موجود؟
        var courseExists = await _context.Courses
            .AnyAsync(c => c.Id == dto.CourseId);

        if (!courseExists)
            return BadRequest("Course not found");

        // تحقق: الطالب مسجّل من قبل؟
        var alreadyEnrolled = await _context.Enrollments
            .AnyAsync(e => e.StudentId == dto.StudentId
                        && e.CourseId == dto.CourseId);

        if (alreadyEnrolled)
            return BadRequest("Student already enrolled in this course");

        var enrollment = _mapper.Map<Enrollment>(dto);
        enrollment.EnrolledAt = DateTime.UtcNow;

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        // حمّل الطالب والكورس عشان الأسماء
        await _context.Entry(enrollment)
            .Reference(e => e.Student)
            .LoadAsync();
        await _context.Entry(enrollment)
            .Reference(e => e.Course)
            .LoadAsync();

        var response = _mapper.Map<EnrollmentResponseDto>(enrollment);
        return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, response);
    }

    // PUT: api/enrollments/1 (تحديث الدرجة فقط)
    [HttpPut("{id}")]
    public async Task<ActionResult<EnrollmentResponseDto>> Update(int id, UpdateEnrollmentDto dto)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrollment == null)
            return NotFound("Enrollment not found");

        enrollment.Grade = dto.Grade;
        await _context.SaveChangesAsync();

        var response = _mapper.Map<EnrollmentResponseDto>(enrollment);
        return Ok(response);
    }

    // DELETE: api/enrollments/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
            return NotFound("Enrollment not found");

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}