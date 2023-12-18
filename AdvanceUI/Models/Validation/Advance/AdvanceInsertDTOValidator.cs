using AdvanceUI.Models.DTO.Advance;
using FluentValidation;
using System;

namespace AdvanceUI.Models.Validation.Advance
{
    public class AdvanceInsertDTOValidator : AbstractValidator<AdvanceInsertDTO>
    {
        public AdvanceInsertDTOValidator()
        {
            RuleFor(x => x.AdvanceAmount)
            .NotNull().WithMessage("Avans tutarı boş olamaz.")
             .GreaterThan(0).WithMessage("Avans tutarı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.AdvanceDescription)
                .NotEmpty().WithMessage("Avans açıklaması boş olamaz.")
                .MaximumLength(100).WithMessage("Avans açıklaması en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.ProjectID)
                .NotNull().WithMessage("Proje boş olamaz.");

            RuleFor(x => x.DesiredDate)
                .NotNull().WithMessage("İstenilen tarih boş olamaz.")
                .GreaterThan(DateTime.Today).WithMessage("İstenilen tarih bugünden ileri bir tarih olmalıdır.");

        }
      

    }
}
