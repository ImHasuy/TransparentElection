using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class RegisteredNationalMinorityCandidate
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int RankInList { get; set; }
    
    [ForeignKey("NationalMinorities")]
    public Guid NationalMinoritiesId { get; set; }
    public NationalMinorities NationalMinorities { get; set; }
}