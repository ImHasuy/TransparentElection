namespace Backend.Entities;

public class RegisteredNationalMinorityCandidate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PartyName { get; set; }
    public int RankInList { get; set; }
}