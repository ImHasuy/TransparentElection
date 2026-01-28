using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class PartyListService : IPartyListService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public PartyListService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> AddPartyToPartyList(PartyListAddDto partyListAddDto)
    {
        var temp = _mapper.Map<PartyList>(partyListAddDto);
        await _context.PartyLists.AddAsync(temp);
        await _context.SaveChangesAsync();
        return $"Succefully added the party to the list with id {temp.Id.ToString()}";
    }
    
    public async Task<string> AddMemberToPartyList(PartyListCandidateAddDto partyListCandidateAddDto)
    {
        var exists = await _context.RegisteredPartyListCandidates.AnyAsync(c=> c.PartyListId == partyListCandidateAddDto.PartyListId && c.RankInList == partyListCandidateAddDto.RankInList);
        if(exists) return "This rank in list is already exists";
        var temp = _mapper.Map<RegisteredPartyListCandidate>(partyListCandidateAddDto);
        await _context.RegisteredPartyListCandidates.AddAsync(temp);
        await _context.SaveChangesAsync();
        return $"Succefully added party list member with Id {temp.Id.ToString()}";
    }
    
    public async Task<List<PartyListGetDto>> GetPartyList()
    {
       var temp = await _context.PartyLists.Include(v=>v.RegisteredCandidates).ToListAsync();
       return _mapper.Map<List<PartyListGetDto>>(temp);
    }
    
    
    
    
}