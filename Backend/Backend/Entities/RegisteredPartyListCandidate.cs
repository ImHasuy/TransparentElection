namespace Backend.Entities;

public class RegisteredPartyListCandidate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PartyName { get; set; }
    public int RankInList { get; set; }
}