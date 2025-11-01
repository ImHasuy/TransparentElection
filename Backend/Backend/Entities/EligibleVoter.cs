namespace Backend.Entities;

public class EligibleVoter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IDCardNumer { get; set; }
    public string ResidenceCardNumer { get; set; }
    public int ZipCode { get; set; }
    
    public Guid VotingDisctictId { get; set; }
    public VotingDistinct VotingDisctict { get; set; }
    
    
}