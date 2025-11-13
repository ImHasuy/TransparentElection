using System.Data.Common;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context;

public class AppDbContext :DbContext
{
    
    public DbSet<Admin> Admins { get; set; }
    public DbSet<EligibleVoter> EligibleVoters { get; set; }
    public DbSet<NationalMinorities> NationalMinorities { get; set; }
    public DbSet<PartyList> PartyLists { get; set; }
    public DbSet<RegisteredNationalMinorityCandidate> RegisteredNationalMinorityCandidates { get; set; }
    public DbSet<RegisteredPartyListCandidate> RegisteredPartyListCandidates { get; set; }
    public DbSet<SingleMemberCandidate> SingleMemberCandidates { get; set; }
    public DbSet<VoterAddress> VoterAddresses { get; set; }
    public DbSet<VotingDistrict> VotingDistricts { get; set; }
    public DbSet<VotingTokens> VotingTokens { get; set; }
    

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //User entity configuration
        /*
        modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        */
        
    }
}