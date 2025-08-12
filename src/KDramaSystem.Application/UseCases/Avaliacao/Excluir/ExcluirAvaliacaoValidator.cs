using FluentValidation;

namespace KDramaSystem.Application.UseCases.Avaliacao.Excluir;

public class ExcluirAvaliacaoValidator : AbstractValidator<ExcluirAvaliacaoRequest>
{
    public ExcluirAvaliacaoValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("A temporada é obrigatória.");
    }
}