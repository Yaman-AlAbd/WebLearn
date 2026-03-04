
using WebLearn.DTOs.Student;
using FluentValidation;
namespace WebLearn.Validators.VaildatorStudent;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(s => s.FullName)
            .NotEmpty().WithMessage("الاسم مطلوب")
            .MinimumLength(2).WithMessage("الاسم لازم أكثر من حرفين")
            .MaximumLength(100).WithMessage("الاسم أقصى حد 100 حرف");

        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("الإيميل مطلوب")
            .EmailAddress().WithMessage("صيغة الإيميل غير صحيحة")
            .MaximumLength(150).WithMessage("الإيميل أقصى حد 150 حرف");

        RuleFor(s => s.Phone)
            .MaximumLength(20).WithMessage("رقم الجوال أقصى حد 20 رقم");

        RuleFor(s => s.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("تاريخ الميلاد لازم يكون في الماضي");
    }
}