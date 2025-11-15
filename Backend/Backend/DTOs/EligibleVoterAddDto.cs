using System.ComponentModel.DataAnnotations.Schema;
using Backend.Entities;
using Backend.Entities.Enum;

namespace Backend.DTOs;

public class EligibleVoterAddDto
{
    public string Name { get; set; }
    public string IDCardNumber { get; set; }
    public string ResidenceCardNumber { get; set; }
    public Guid VoterAddressId { get; set; }
    public Guid VotingDistinctId { get; set; }
    public bool IsNationalMinorityVoter { get; set; } = false;
    public NationalMinoritiesEnum NationalMinoritiesEnum { get; set; } = NationalMinoritiesEnum.None;
}