namespace Backend.Entities;

public class SingleMemberConstituecy
{
    public Guid Id { get; set; }
    public string PartyName { get; set; }
    
    public Guid VotingDistinctId { get; set; }
    public VotingDistinct VotingDistinct { get; set; }
    
    public List<RegisteredSingleMemberCandidate> RegisteredCandidates { get; set; }
}