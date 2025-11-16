using Backend.DTOs;

namespace Backend.interfaces;

public interface IEligibleVoterService
{
    Task<string> AddEligibleVoter(EligibleVoterAddDto eligibleVoterAddDto);
    Task<List<VoterAddressesGetDto>> GetAddressOptions(VoterAddressesPostInputDto voterAddressesPostInputDto);
}