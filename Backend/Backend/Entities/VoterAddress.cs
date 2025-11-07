using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Entities;

[Index(nameof(ZipCode),IsUnique = false)]
[Index(nameof(CityName),IsUnique = false)]
[Index(nameof(StreetName),IsUnique = false)]
[Index(nameof(StreetType),IsUnique = false)]
[Index(nameof(HouseNumberStart),IsUnique = false)]
[Index(nameof(HouseNumberEnd),IsUnique = false)]
public class VoterAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ZipCode { get; set; }
    public string CityName { get; set; }
    public string StreetName { get; set; }
    public string StreetType { get; set; }
    public int HouseNumberStart { get; set; }
    public int HouseNumberEnd { get; set; }
    public string? Building { get; set; } 
    public string? Staircase { get; set; }
    
    [ForeignKey("VotingDistrict")]
    public Guid VotingDistrictId { get; set; }
    public VotingDistrict VotingDistrict { get; set; }
}