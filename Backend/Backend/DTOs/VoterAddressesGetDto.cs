namespace Backend.DTOs;

public class VoterAddressesGetDto
{
    public Guid VoterAddressId { get; set; }
    public int HouseNumber { get; set; }
    public Guid VotingDistrictId { get; set; }
}