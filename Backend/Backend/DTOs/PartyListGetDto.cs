namespace Backend.DTOs;

public class PartyListGetDto
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string LogoPath { get; set; }
    public string Color { get; set; }
    public string FirstOnList { get; set; }
}