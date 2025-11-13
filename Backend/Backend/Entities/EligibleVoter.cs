using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Backend.Entities;

[Index(nameof(IDCardNumber),IsUnique = true)]
[Index(nameof(ResidenceCardNumber),IsUnique = true)]
[Index(nameof(IsNationalMinorityVoter))]
public class EligibleVoter
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; }
    [Required]
    public string IDCardNumber { get; set; }
    [Required]
    public string ResidenceCardNumber { get; set; }
    [Required]
    
    [ForeignKey("VoterAddress")]
    public Guid VoterAddressId { get; set; }
    public VoterAddress VoterAddress { get; set; }
    
    public bool IsNationalMinorityVoter { get; set; } = false;
    public NationalMinoritiesEnum NationalMinoritiesEnum { get; set; } = NationalMinoritiesEnum.None;
    
    [ForeignKey("VotingDistrict")]
    public Guid VotingDistinctId { get; set; }
    public VotingDistrict VotingDistrict { get; set; }
}