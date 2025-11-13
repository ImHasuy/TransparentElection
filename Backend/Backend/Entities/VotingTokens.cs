using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Entities;

[Index(nameof(VotingToken),IsUnique = true)]
public class VotingTokens
{
    [Key, Required]
    public Guid VotingToken { get; set; } = Guid.NewGuid();
    
    [ForeignKey("VotingDistrict")]
    public Guid VotingDistrictId { get; set; }
    public VotingDistrict VotingDistrict { get; set; }
}