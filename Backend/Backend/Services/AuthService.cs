using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    public AuthService(AppDbContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }
     
    
        public async Task<LoginResponseDto> AuthenticateAsyncForVoter(FirstLayerLoginDto firstLayerLoginDto)
        {
            var user = await _context.EligibleVoters
                .FirstOrDefaultAsync(u => u.IDCardNumber == firstLayerLoginDto.IDCardNumber 
                                          && u.ResidenceCardNumber == firstLayerLoginDto.ResidenceCardNumber) 
                       ?? throw new Exception("Eligible Voter not found with these credentials");
            return new LoginResponseDto
            {
                token = await GenerateTokenForVoter(user)
            };
        }
        
        private async Task<string> GenerateTokenForVoter(EligibleVoter eligibleVoter)
        {
            var id = await GetClaimsIdentityForVoter(eligibleVoter);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpiresInMinutes"]));
            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"], _config["JwtSettings:Audience"], id.Claims, expires: exp, signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
        private Task<ClaimsIdentity> GetClaimsIdentityForVoter(EligibleVoter eligibleVoter)
        {
            var claims = new List<Claim>
            {
                new Claim("IDCardNumber", eligibleVoter.IDCardNumber),
                new Claim("ResidenceCardNumber", eligibleVoter.ResidenceCardNumber),
                new Claim("VotingDistrict", eligibleVoter.VotingDistinctId.ToString()),
                new Claim("IsNationalMinorities", eligibleVoter.IsNationalMinorityVoter.ToString()),
                new Claim("NationalMinoritiesEnum", eligibleVoter.NationalMinoritiesEnum.ToString()),
                
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture)),
            };
            return Task.FromResult(new ClaimsIdentity(claims, "Token"));
        }

    
    
    
}