using WebLearn.DTOs.Course;
using FluentValidation;


namespace WebLearn.Validators.CourseValidator;

public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("عنوان الكورس مطلوب")
            .MinimumLength(2).WithMessage("العنوان لازم أكثر من حرفين")
            .MaximumLength(200).WithMessage("العنوان أقصى حد 200 حرف");

        RuleFor(c => c.Description)
            .MaximumLength(1000).WithMessage("الوصف أقصى حد 1000 حرف");

        RuleFor(c => c.Price)
            .GreaterThanOrEqualTo(0).WithMessage("السعر لازم يكون 0 أو أكثر");

        RuleFor(c => c.MaxStudents)
            .GreaterThan(0).WithMessage("عدد الطلاب لازم أكثر من 0")
            .LessThanOrEqualTo(500).WithMessage("عدد الطلاب أقصى حد 500");

        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("تاريخ البداية مطلوب");

        RuleFor(c => c.TeacherId)
            .GreaterThan(0).WithMessage("لازم تختار معلم");
    }
}