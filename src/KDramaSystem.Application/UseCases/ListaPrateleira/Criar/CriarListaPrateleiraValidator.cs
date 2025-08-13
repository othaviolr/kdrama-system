using FluentValidation;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Criar;

public class CriarListaPrateleiraValidator : AbstractValidator<CriarListaPrateleiraRequest>
{
    public CriarListaPrateleiraValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da lista é obrigatório.")
            .MaximumLength(200).WithMessage("O nome da lista não pode ter mais de 200 caracteres.");

        RuleFor(x => x.Descricao)
            .MaximumLength(1000).WithMessage("A descrição não pode ter mais de 1000 caracteres.");

        RuleFor(x => x.ImagemCapaUrl)
            .MaximumLength(500).WithMessage("A URL da imagem de capa não pode ter mais de 500 caracteres.");

        RuleFor(x => x.Privacidade)
            .IsInEnum().WithMessage("Privacidade inválida.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O usuário que está criando a lista precisa ser informado.");
    }
}