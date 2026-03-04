using AutoMapper;
using WebLearn.DTOs.Student;
using WebLearn.DTOs.Teacher;
using WebLearn.DTOs.Course;
using WebLearn.DTOs.Enrollment;
using WebLearn.Entities;

namespace WebLearn.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentResponseDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
        
        
        // ──────────── Teacher ────────────
        CreateMap<Teacher, TeacherResponseDto>();
        CreateMap<CreateTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();

        // ──────────── Course ─────────────
        CreateMap<Course, CourseResponseDto>()
            .ForMember(
                dest => dest.TeacherName,
                opt => opt.MapFrom(src => src.Teacher.FullName)
            );
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        // ──────────── Enrollment ─────────
        CreateMap<Enrollment, EnrollmentResponseDto>()
            .ForMember(
                dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student.FullName)
            )
            .ForMember(
                dest => dest.CourseTitle,
                opt => opt.MapFrom(src => src.Course.Title)
            );
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
        
        
        CreateMap<Course, CourseStudentCountDto>()
            .ForMember(d => d.TeacherId,
                opt => opt.MapFrom(s => s.TeacherId))
            .ForMember(d => d.CourseId,
                opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CourseTitle,
                opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.StudentCount,
                opt => opt.MapFrom(s =>
                    s.Enrollments
                        .Select(e => e.StudentId)
                        .Distinct()
                        .Count()
                ));
        
        
        
        CreateMap<Enrollment, StudentCourseDto>()
            
            .ForMember(d => d.CourseTitle, opt => opt.MapFrom(s => s.Course.Title))
            .ForMember(d => d.TeacherName, opt => opt.MapFrom(s => s.Course.Teacher.FullName));

// Student -> StudentWithCoursesDto
        CreateMap<Student, StudentWithCoursesDto>()
            .ForMember(d => d.Courses, opt => opt.MapFrom(s => s.Enrollments));
        
        
        CreateMap<Student, StudentWithCoursesDto>()
            .ForMember(d => d.Courses, opt => opt.MapFrom(s => s.Enrollments))
            .AfterMap((src, dest) =>
            {
                // 1) ترتيب الكورسات بعد ما تم تعبئتها
                dest.Courses = dest.Courses
                    .OrderBy(c => c.CourseTitle)
                    .ToList();

                // 2) حساب عدد الكورسات
                dest.CoursesCount = dest.Courses.Count;
            });
        
        
    }
    
    
}