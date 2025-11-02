using System.ComponentModel.DataAnnotations;
using Backend.Entities.Enum;

namespace Backend.Entities;

public class NationalMinorities
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string PartyName { get; set; }
    public string LogoPath { get; set; }
    public string Description { get; set; }
    public NationalMinoritiesEnum NationalMinoritiesType { get; set; }

    public List<RegisteredNationalMinorityCandidate> RegisteredCandidates { get; set; }
}