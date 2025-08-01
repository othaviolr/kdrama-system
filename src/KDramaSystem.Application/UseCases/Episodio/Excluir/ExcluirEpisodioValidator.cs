using FluentValidation;

namespace KDramaSystem.Application.UseCases.Episodio.Excluir;

public class ExcluirEpisodioValidator : AbstractValidator<ExcluirEpisodioRequest>
{
    public ExcluirEpisodioValidator()
    {
        RuleFor(x => x.EpisodioId)
            .NotEmpty().WithMessage("Id do episódio é obrigatório.");
    }
}