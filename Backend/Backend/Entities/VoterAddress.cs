using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class VoterAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ZipCode { get; set; }
    public string StreetName { get; set; }
    public string StreetType { get; set; }
    public string HouseNumber { get; set; }
    public string? Building { get; set; } 
    public string? Staircase { get; set; }
    
    [ForeignKey("VotingDistrict")]
    public Guid VotingDistrictId { get; set; }
    public VotingDistrict VotingDistrict { get; set; }
}