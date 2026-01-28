namespace Backend.DTOs;

public class VoterInputDto
{
    public string ZipCode { get; set; }
    public string StreetName { get; set; }
    public string StreetType { get; set; }
    public int HouseNumber { get; set; }
    public string? Building { get; set; } 
    public string? Staircase { get; set; }
}