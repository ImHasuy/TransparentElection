namespace Backend.Entities;

public class BlockchainVoteInputDto
{
    public string PartyVote { get; set; }
    public string SingleMemberVote { get; set; }
    public string votingToken { get; set; }
    public string Signature { get; set; }
    public string UserAddress { get; set; }
}