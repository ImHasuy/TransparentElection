using System.Security.Claims;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SingleMemberCandidateService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> AddSingleMemberCandidate(SingleMemberCandidateAddDto singleMemberCandidateAddDto)
    {
        var temp = _mapper.Map<SingleMemberCandidate>(singleMemberCandidateAddDto);
        await _context.SingleMemberCandidates.AddAsync(temp);
        await _context.SaveChangesAsync();
        return $"Succefully added the candidate with id {temp.Id.ToString()}";
    }
    
    private string? RequesterVotingDistrict =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue("VotingDistrict");
    
    public async Task<List<SingleMemberCandidatesGetDto>> GetCandidatesForVotingDistrict()
    {
        var temp = await _context.SingleMemberCandidates.Where(c =>
            c.VotingDistinctId.ToString() == RequesterVotingDistrict)
            .ToListAsync();

        var mapped = _mapper.Map<List<SingleMemberCandidatesGetDto>>(temp);
        return mapped;
    }
}