using FluentValidation;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Editar;

public class EditarListaPrateleiraValidator : AbstractValidator<EditarListaPrateleiraRequest>
{
    public EditarListaPrateleiraValidator()
    {
        RuleFor(x => x.ListaId)
            .NotEmpty().WithMessage("O Id da lista é obrigatório.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O Id do usuário é obrigatório.");

        RuleFor(x => x.Nome)
            .MaximumLength(200).WithMessage("O nome da lista não pode ter mais de 200 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Nome));

        RuleFor(x => x.Descricao)
            .MaximumLength(1000).WithMessage("A descrição não pode ter mais de 1000 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Descricao));

        RuleFor(x => x.ImagemCapaUrl)
            .MaximumLength(500).WithMessage("A URL da imagem de capa não pode ter mais de 500 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.ImagemCapaUrl));

        RuleFor(x => x.Privacidade)
            .IsInEnum().WithMessage("Privacidade inválida.")
            .When(x => x.Privacidade.HasValue);
    }
}