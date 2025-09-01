using FluentValidation;
using KDramaSystem.Domain.Interfaces;
using static KDramaSystem.Application.DTOs.Dorama.ObterDoramaDto;

namespace KDramaSystem.Application.UseCases.Ator.Obter;

public class ObterAtorUseCase
{
    private readonly IAtorRepository _atorRepository;
    private readonly IValidator<ObterAtorRequest> _validator;

    public ObterAtorUseCase(IAtorRepository atorRepository, IValidator<ObterAtorRequest> validator)
    {
        _atorRepository = atorRepository;
        _validator = validator;
    }

    public async Task<Domain.Entities.Ator> ExecutarAsync(ObterAtorRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var ator = await _atorRepository.ObterPorIdAsync(request.Id);
        if (ator == null)
            throw new Exception("Ator não encontrado.");

        return ator;
    }

    public async Task<AtorDto?> ExecutarPorNomeAsync(string nome)
    {
        var ator = await _atorRepository.ObterPorNomeAsync(nome);
        if (ator == null) return null;

        return new AtorDto
        {
            Id = ator.Id,
            Nome = ator.Nome,
            FotoUrl = ator.FotoUrl
        };
    }
}