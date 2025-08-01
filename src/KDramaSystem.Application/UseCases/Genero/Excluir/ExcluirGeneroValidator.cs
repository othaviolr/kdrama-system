using FluentValidation;

namespace KDramaSystem.Application.UseCases.Genero.Excluir;

public class ExcluirGeneroValidator : AbstractValidator<ExcluirGeneroRequest>
{
    public ExcluirGeneroValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do gênero é obrigatório.");
    }
}