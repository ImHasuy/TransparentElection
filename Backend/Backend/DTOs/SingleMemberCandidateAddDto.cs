namespace Backend.DTOs;

public class SingleMemberCandidateAddDto
{
    public string Name { get; set; }
    public string PartyName { get; set; }
    public string FotoPah { get; set; }
    public Guid VotingDistinctId { get; set; }
}