using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class VotingDistrict
{
    [Key,Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string CountyCode { get; set; }
    public string CountyName { get; set; }
    public string OEVK { get; set; }
    public string CityCode { get; set; }
    public string CityName { get; set; }
    public string TEVK { get; set; }
    public string PollingStationNumber { get; set; }
    public string PollingStationAddress{ get; set; }

    public List<VoterAddress> VoterAddresses { get; set; } = [];
    public List<EligibleVoter>? EligibleVoters { get; set; }
    public List<SingleMemberCandidate>? SingleMemberCandidates { get; set; }
}