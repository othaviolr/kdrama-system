using FluentValidation;

namespace KDramaSystem.Application.UseCases.Ator.Editar;

public class EditarAtorValidator : AbstractValidator<EditarAtorRequest>
{
    public EditarAtorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id é obrigatório.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(2).WithMessage("Nome deve ter pelo menos 2 caracteres.");

        RuleFor(x => x.AnoNascimento)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.AnoNascimento.HasValue)
            .WithMessage("Ano de nascimento inválido.");

        RuleFor(x => x.Altura)
            .GreaterThan(0)
            .When(x => x.Altura.HasValue)
            .WithMessage("Altura deve ser um valor positivo.");
    }
}