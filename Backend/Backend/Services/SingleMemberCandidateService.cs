using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class SingleMemberCandidateService : ISingleMemberCandidateService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public SingleMemberCandidateService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> AddSingleMemberCandidate(SingleMemberCandidateAddDto singleMemberCandidateAddDto)
    {
        var temp = _mapper.Map<SingleMemberCandidate>(singleMemberCandidateAddDto);
        await _context.SingleMemberCandidates.AddAsync(temp);
        await _context.SaveChangesAsync();
        return $"Succefully added the candidate with id {temp.Id.ToString()}";
    }
    
    
    public async Task<List<SingleMemberCandidatesGetDto>> GetCandidatesForVotingDistrict(SingleMemberCandidatesInputGetDto singleMemberCandidatesInputGetDto)
    {
        var temp = await _context.SingleMemberCandidates.Where(c =>
            c.VotingDistinctId.ToString() == singleMemberCandidatesInputGetDto.VotingDistinctId)
            .ToListAsync();

        var mapped = _mapper.Map<List<SingleMemberCandidatesGetDto>>(temp);
        return mapped;
    }
}