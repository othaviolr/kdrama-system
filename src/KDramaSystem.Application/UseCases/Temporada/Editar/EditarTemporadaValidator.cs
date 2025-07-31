using FluentValidation;

namespace KDramaSystem.Application.UseCases.Temporada.Editar;

public class EditarTemporadaValidator : AbstractValidator<EditarTemporadaRequest>
{
    public EditarTemporadaValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.AnoLancamento)
            .InclusiveBetween(1900, DateTime.UtcNow.Year + 1);
        RuleFor(x => x.Nome)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.Nome));
    }
}