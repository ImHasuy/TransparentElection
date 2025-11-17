using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class PartyList
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string LogoPath { get; set; }
    public string Color { get; set; }
    
    public List<RegisteredPartyListCandidate> RegisteredCandidates { get; set; }
}