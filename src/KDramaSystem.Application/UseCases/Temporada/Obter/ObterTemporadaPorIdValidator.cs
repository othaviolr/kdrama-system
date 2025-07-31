using FluentValidation;

namespace KDramaSystem.Application.UseCases.Temporada.Obter;

public class ObterTemporadaPorIdValidator : AbstractValidator<ObterTemporadaPorIdRequest>
{
    public ObterTemporadaPorIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O ID da temporada é obrigatório.");
    }
}