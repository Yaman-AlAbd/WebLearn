using WebLearn.DTOs.Enrollment;
using FluentValidation;

namespace WebLearn.Validators.EnrollmentValidator;

public class UpdateEnrollmentValidator : AbstractValidator<UpdateEnrollmentDto>
{
    public UpdateEnrollmentValidator()
    {
        RuleFor(e => e.Grade)
            .InclusiveBetween(0, 100)
            .WithMessage("الدرجة لازم تكون بين 0 و 100")
            .When(e => e.Grade.HasValue);
    }
}