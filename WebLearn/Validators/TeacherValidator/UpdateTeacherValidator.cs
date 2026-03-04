using WebLearn.DTOs.Teacher;
using FluentValidation;

namespace WebLearn.Validators.TeacherValidator;

public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherDto>
{
    public UpdateTeacherValidator()
    {
        RuleFor(t => t.FullName)
            .NotEmpty().WithMessage("اسم المعلم مطلوب")
            .MinimumLength(2).WithMessage("الاسم لازم أكثر من حرفين")
            .MaximumLength(100).WithMessage("الاسم أقصى حد 100 حرف");

        RuleFor(t => t.Email)
            .NotEmpty().WithMessage("الإيميل مطلوب")
            .EmailAddress().WithMessage("صيغة الإيميل غير صحيحة")
            .MaximumLength(150).WithMessage("الإيميل أقصى حد 150 حرف");

        RuleFor(t => t.Phone)
            .MaximumLength(20).WithMessage("رقم الجوال أقصى حد 20 رقم");

        RuleFor(t => t.Specialty)
            .NotEmpty().WithMessage("التخصص مطلوب")
            .MaximumLength(100).WithMessage("التخصص أقصى حد 100 حرف");
    }
}