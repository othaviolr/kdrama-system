using FluentValidation;

namespace KDramaSystem.Application.UseCases.Temporada.Criar;

public class CriarTemporadaValidator : AbstractValidator<CriarTemporadaRequest>
{
    public CriarTemporadaValidator()
    {
        RuleFor(x => x.DoramaId).NotEmpty();
        RuleFor(x => x.Numero).GreaterThan(0);
        RuleFor(x => x.AnoLancamento)
            .InclusiveBetween(1900, DateTime.UtcNow.Year + 1);
    }
}