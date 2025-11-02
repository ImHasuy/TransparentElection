namespace Backend.Entities;

public class PartyList
{
    public Guid Id { get; set; }
    public string PartyName { get; set; }
    public string LogoPath { get; set; }
    
    public List<RegisteredPartyListCandidate> RegisteredCandidates { get; set; }
}