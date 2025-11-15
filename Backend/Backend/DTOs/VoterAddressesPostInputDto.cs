namespace Backend.DTOs;

public class VoterAddressesPostInputDto
{
    public string ZipCode { get; set; }
    public string CityName { get; set; }
    public string StreetName { get; set; }
    public string StreetType { get; set; }
}