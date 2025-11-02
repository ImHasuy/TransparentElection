namespace Backend.Entities;

public class RegisteredSingleMemberCandidate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PartyName { get; set; }
    
    public Guid VotingDistinctId  { get; set; }
    public VotingDistinct VotingDistinct { get; set; }
    
    public SingleMemberConstituecy SingleMemberConstituecy { get; set; }
}