using WebLearn.DTOs.Enrollment;
using FluentValidation;
namespace WebLearn.Validators.EnrollmentValidator;

public class CreateEnrollmentValidator : AbstractValidator<CreateEnrollmentDto>
{
    public CreateEnrollmentValidator()
    {
        RuleFor(e => e.StudentId)
            .GreaterThan(0).WithMessage("لازم تختار طالب");

        RuleFor(e => e.CourseId)
            .GreaterThan(0).WithMessage("لازم تختار كورس");
    }
}