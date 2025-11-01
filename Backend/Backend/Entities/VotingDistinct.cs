namespace Backend.Entities;

public class VotingDistinct
{
    public Guid Id { get; set; }
    public string DistinctName { get; set; }
    
    public List<EligibleVoter> EligibleVoters { get; set; }
}