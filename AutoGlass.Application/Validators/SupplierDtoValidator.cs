using AutoGlass.Application.Dtos;
using FluentValidation;

namespace AutoGlass.Application.Validators
{
    public class SupplierDtoValidator : AbstractValidator<SupplierDto>
    {
        public SupplierDtoValidator()
        {
            RuleFor(p => p.IsActive)
                .NotEqual(false).WithMessage("Não é possível alterar dados de um produto deletado.");

            RuleFor(s => s.Description)
                .NotNull().WithMessage("A descrição do fornecedor deve ser preenchida.")
                .NotEmpty().WithMessage("A descrição do fornecedor deve ser preenchida.");

            RuleFor(s => s.Cnpj)
                .NotNull().WithMessage("O Cnpj do fornecedor deve ser preenchida.")
                .NotEmpty().WithMessage("O Cnpj do fornecedor deve ser preenchida.");
        }
    }
}