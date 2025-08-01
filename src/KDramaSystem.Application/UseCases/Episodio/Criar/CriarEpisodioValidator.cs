using FluentValidation;

namespace KDramaSystem.Application.UseCases.Episodio.Criar;

public class CriarEpisodioValidator : AbstractValidator<CriarEpisodioRequest>
{
    public CriarEpisodioValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("TemporadaId é obrigatório.");

        RuleFor(x => x.Numero)
            .GreaterThan(0).WithMessage("Número do episódio deve ser maior que zero.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório.");

        RuleFor(x => x.DuracaoMinutos)
            .GreaterThan(0).WithMessage("Duração deve ser maior que zero minutos.");
    }
}