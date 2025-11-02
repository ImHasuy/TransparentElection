using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class RegisteredPartyListCandidate
{
    [Key,Required]
    public Guid Id { get; set; }  = Guid.NewGuid();
    public string Name { get; set; }
    public int RankInList { get; set; }
    
    [ForeignKey("PartyList")]
    public Guid PartyListId { get; set; }
    public PartyList PartyList { get; set; }
    
}




