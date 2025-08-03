using FluentValidation;

namespace KDramaSystem.Application.UseCases.Ator.Excluir;

public class ExcluirAtorValidator : AbstractValidator<ExcluirAtorRequest>
{
    public ExcluirAtorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id é obrigatório.");
    }
}