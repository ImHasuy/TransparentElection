namespace Backend.DTOs;

public class VoteRequestDto
{
    public string PartyVote { get; set; }
    public string SingleMemberVote { get; set; }
    public string VotingToken { get; set; }
    public string Signature { get; set; }
    public string UserAddress { get; set; }
}