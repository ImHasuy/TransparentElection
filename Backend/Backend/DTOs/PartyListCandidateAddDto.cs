namespace Backend.DTOs;

public class PartyListCandidateAddDto
{
    public string Name { get; set; }
    public int RankInList { get; set; }
    public Guid PartyListId { get; set; }
}