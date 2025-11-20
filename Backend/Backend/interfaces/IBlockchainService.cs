using Backend.Entities;

namespace Backend.interfaces;

public interface IBlockchainService
{
    Task<string> CommitVoteToBlockchain(BlockchainVoteInputDto blockchainVoteInputDto);
    Task<string> GetVotesForParty(string input);
}