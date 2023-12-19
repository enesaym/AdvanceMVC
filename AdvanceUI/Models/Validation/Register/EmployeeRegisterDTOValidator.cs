using AdvanceUI.Models.DTO.Employee;
using FluentValidation;

namespace AdvanceUI.Models.Validation.Register
{
    public class EmployeeRegisterDTOValidator : AbstractValidator<EmployeeRegisterDTO>
    {
        public EmployeeRegisterDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.");

            RuleFor(e => e.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası alanı boş olamaz.")
                .Matches(@"^\+[1-9]{1}[0-9]{3,14}$")
                .WithMessage("Geçersiz telefon numarası formatı. Örnek: +1234567890");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.");

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Şifre en az bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.");
        }
    }
}
