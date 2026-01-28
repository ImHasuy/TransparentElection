using Backend.DTOs;

namespace Backend.interfaces;

public interface IPartyListService
{
    Task<string> AddPartyToPartyList(PartyListAddDto partyListAddDto);
    Task<string> AddMemberToPartyList(PartyListCandidateAddDto partyListCandidateAddDto);
    Task<List<PartyListGetDto>> GetPartyList();
}