using System.Numerics;
using AutoMapper;
using Backend.Context;
using Backend.Entities;
using Backend.interfaces;
using Nethereum.ABI;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;

namespace Backend.Services;

public class BlockchainService: IBlockchainService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    
    private readonly string _contractAddress = "0x5FbDB2315678afecb367f032d93F642f64180aa3";
    private readonly string _backendPrivateKey = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
    private readonly string _rpcUrl = "http://127.0.0.1:8545";

    
    public BlockchainService(AppDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> CommitVoteToBlockchain(BlockchainVoteInputDto blockchainVoteInputDto)
    {
        try
        {
            var isValid = VerifySignature(blockchainVoteInputDto);
            
            if(!isValid) throw new Exception("Signature is not valid");


            await SendTransactionToBlockchain(blockchainVoteInputDto);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured {e.Message}");
        }

        return "ok";
    }
    
    
    
    public async Task<string> GetVotesForParty(string input)
    {
        
      var account = new Account(_backendPrivateKey);
      var web3 = new Web3(account, _rpcUrl);

      
      var contract = web3.Eth.GetContract(GetVotingAbi(), _contractAddress);
        
      var getPartyCountFunction = contract.GetFunction("getPartyCount");
      
      
      var voteCount = await getPartyCountFunction.CallAsync<System.Numerics.BigInteger>(input);

      var returnString = $"Number of votes for party {input} is: {voteCount.ToString()}";
      return returnString;

    }

    
    private async Task<string> SendTransactionToBlockchain(BlockchainVoteInputDto input)
    {
        var account = new Account(_backendPrivateKey);
        var web3 = new Web3(account, _rpcUrl);

      
        var contract = web3.Eth.GetContract(GetVotingAbi(), _contractAddress);
        
        var voteFunction = contract.GetFunction("vote");
        
        var receipt = await voteFunction.SendTransactionAndWaitForReceiptAsync(
            account.Address, 
            new HexBigInteger(900000), 
            new HexBigInteger(0),
            null, 
            input.PartyVote,
            input.SingleMemberVote,
            input.UserAddress 
        );

        return receipt.TransactionHash;
    }
    
    private bool VerifySignature(BlockchainVoteInputDto blockchainVoteInputDto)
    {
        var abiEncode = new ABIEncode();
        var packedBytes = abiEncode.GetABIEncodedPacked(
            new ABIValue("string", new string(blockchainVoteInputDto.PartyVote)),
            new ABIValue("string", new string(blockchainVoteInputDto.SingleMemberVote)),
            new ABIValue("address", _contractAddress)
        );

        var sha3 = new Sha3Keccack();
        var messageHash = sha3.CalculateHash(packedBytes);
        
        var signer = new EthereumMessageSigner();
        try
        {
            var recoveredAddress = signer.EcRecover(messageHash, blockchainVoteInputDto.Signature);
            return recoveredAddress.Equals(blockchainVoteInputDto.UserAddress, StringComparison.InvariantCultureIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private string GetVotingAbi()
    {
        return @"[
    {
      ""inputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""constructor""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": true,
          ""internalType"": ""address"",
          ""name"": ""voter"",
          ""type"": ""address""
        },
        {
          ""indexed"": false,
          ""internalType"": ""string"",
          ""name"": ""party"",
          ""type"": ""string""
        },
        {
          ""indexed"": false,
          ""internalType"": ""string"",
          ""name"": ""member"",
          ""type"": ""string""
        },
        {
          ""indexed"": false,
          ""internalType"": ""uint256"",
          ""name"": ""timestamp"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""VoteCast"",
      ""type"": ""event""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""_memberId"",
          ""type"": ""string""
        }
      ],
      ""name"": ""getMemberCount"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""_partyId"",
          ""type"": ""string""
        }
      ],
      ""name"": ""getPartyCount"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""address"",
          ""name"": ""_user"",
          ""type"": ""address""
        }
      ],
      ""name"": ""getUserVote"",
      ""outputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""party"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""member"",
          ""type"": ""string""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""name"": ""memberVoteCounts"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [],
      ""name"": ""owner"",
      ""outputs"": [
        {
          ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""name"": ""partyVoteCounts"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""_party"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_member"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""address"",
          ""name"": ""_voter"",
          ""type"": ""address""
        }
      ],
      ""name"": ""vote"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""address"",
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""name"": ""votes"",
      ""outputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""partyVote"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""singleMemberVote"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""bool"",
          ""name"": ""hasVoted"",
          ""type"": ""bool""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""timestamp"",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    }
  ]";
    }

}