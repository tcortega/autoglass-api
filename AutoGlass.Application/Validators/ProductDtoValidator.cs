using AutoGlass.Application.Dtos;
using FluentValidation;

namespace AutoGlass.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.IsActive)
                .NotEqual(false).WithMessage("Não é possível alterar dados de um produto deletado.");

            RuleFor(p => p.ManufactureDate)
                .NotEqual(p => p.ExpirationDate).WithMessage("A data de fabricação do produto não pode ser a mesma que a data de validade.")
                .LessThan(p => p.ExpirationDate).WithMessage("A data de fabricação do produto não pode ser maior que a data de validade.")
                .NotNull().WithMessage("A data de fabricação do produto é de preenchimento obrigatório.");

            RuleFor(p => p.ExpirationDate)
                .NotNull().WithMessage("A data de validade do produto é de preenchimento obrigatório.");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("A descrição do produto deve ser preenchida.")
                .NotEmpty().WithMessage("A descrição do produto deve ser preenchida.");

            RuleFor(p => p.Supplier.Id)
                .NotNull().WithMessage("O id do fornecedor deve ser preenchido ao tentar cadastrar um produto.")
                .GreaterThan(0).WithMessage("O id do fornecedor deve ser preenchido ao tentar cadastrar um produto.");
        }
    }
}