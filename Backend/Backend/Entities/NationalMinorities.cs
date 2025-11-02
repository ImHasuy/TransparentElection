namespace Backend.Entities;

public class NationalMinorities
{
    public Guid Id { get; set; }
    public string PartyName { get; set; }
    public string LogoPath { get; set; }
    public string Description { get; set; }

    public List<RegisteredNationalMinorityCandidate> RegisteredCandidates { get; set; }
}