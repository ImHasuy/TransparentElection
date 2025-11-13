using Backend.DTOs;

namespace Backend.interfaces;

public interface IImportAndGenerationService
{
    public Task<string> LoadDistinctsFromFile(string path);
    public Task<string> WhereAmI(VoterInputDto address);
    public Task<string> LoadVoterCount(string path);
    public Task<string> GenerateTokensForDistricts();
}