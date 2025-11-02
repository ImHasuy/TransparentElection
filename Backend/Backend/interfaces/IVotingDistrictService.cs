using Backend.DTOs;

namespace Backend.interfaces;

public interface IVotingDistrictService
{
    public Task<string> LoadDistinctsFromFile(string path);
    public Task<string> WhereAmI(VoterInputDto address);
}