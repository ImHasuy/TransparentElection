using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class SingleMemberCandidate
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string PartyName { get; set; }
    
    [ForeignKey("VotingDistrict")]
    public Guid VotingDistinctId { get; set; }
    public VotingDistrict VotingDistrict { get; set; }
    
}
