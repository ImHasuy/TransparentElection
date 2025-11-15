using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class EligibleVoterService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public EligibleVoterService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> AddEligibleVoter(EligibleVoterAddDto eligibleVoterAddDto)
    {
        var tempVoter = new EligibleVoter
        {
            Name = eligibleVoterAddDto.Name,
            IDCardNumber = eligibleVoterAddDto.IDCardNumber,
            ResidenceCardNumber = eligibleVoterAddDto.ResidenceCardNumber,
            VoterAddressId = eligibleVoterAddDto.VoterAddressId,
            VotingDistinctId = eligibleVoterAddDto.VotingDistinctId,
            IsNationalMinorityVoter = eligibleVoterAddDto.IsNationalMinorityVoter,
            NationalMinoritiesEnum = eligibleVoterAddDto.NationalMinoritiesEnum
        };

        await _context.EligibleVoters.AddAsync(tempVoter);
        await _context.SaveChangesAsync();
        return $"Succefully added voter with name {tempVoter.Id.ToString()}";
    }

    
    public async Task<List<VoterAddressesGetDto>> GetAddressOptions(VoterAddressesPostInputDto voterAddressesPostInputDto)
    {
        var possibleAddresses = await _context.VoterAddresses.Where(c =>
            c.ZipCode.ToLower().Equals(voterAddressesPostInputDto.ZipCode.ToLower()) &&
            c.CityName.ToLower().Equals(voterAddressesPostInputDto.CityName.ToLower()) &&
            c.StreetName.ToLower().Equals(voterAddressesPostInputDto.StreetName.ToLower()) &&
            c.StreetType.ToLower().Equals(voterAddressesPostInputDto.StreetType.ToLower()))
            .ToListAsync();

        var returnDto = new List<VoterAddressesGetDto>();

        foreach (var address in possibleAddresses)
        {
            if (address.HouseNumberStart != address.HouseNumberEnd)
            {
                for (var i = address.HouseNumberStart; i <= address.HouseNumberEnd; i++)
                {
                    returnDto.Add(new VoterAddressesGetDto
                    {
                        VoterAddressId = address.Id,
                        HouseNumber = i,
                        VotingDistrictId = address.VotingDistrictId
                    });
                }
            }
            else
            {
                returnDto.Add(new VoterAddressesGetDto
                {
                    VoterAddressId = address.Id,
                    HouseNumber = address.HouseNumberStart,
                    VotingDistrictId = address.VotingDistrictId
                });
            }
        }
        return returnDto;
    }
    
}