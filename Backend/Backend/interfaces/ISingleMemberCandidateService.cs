using Backend.DTOs;

namespace Backend.interfaces;

public interface ISingleMemberCandidateService
{
    Task<string> AddSingleMemberCandidate(SingleMemberCandidateAddDto singleMemberCandidateAddDto);

    Task<List<SingleMemberCandidatesGetDto>> GetCandidatesForVotingDistrict();
}