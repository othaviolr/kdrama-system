using FluentValidation;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Ator.Criar;

public class CriarAtorUseCase
{
    private readonly IAtorRepository _atorRepository;
    private readonly IValidator<CriarAtorRequest> _validator;

    public CriarAtorUseCase(IAtorRepository atorRepository, IValidator<CriarAtorRequest> validator)
    {
        _atorRepository = atorRepository;
        _validator = validator;
    }

    public async Task<Guid> ExecutarAsync(CriarAtorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var existe = await _atorRepository.ExisteComNomeAsync(request.Nome);
        if (existe)
            throw new Exception("Já existe um ator com esse nome.");

        var ator = new KDramaSystem.Domain.Entities.Ator(Guid.NewGuid(), request.Nome, request.NomeCompleto, request.AnoNascimento,
            request.Altura, request.Pais, request.Biografia, request.FotoUrl, request.Instagram);

        await _atorRepository.AdicionarAsync(ator);

        return ator.Id;
    }
}