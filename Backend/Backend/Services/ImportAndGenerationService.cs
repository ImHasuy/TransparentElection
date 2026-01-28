using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;


namespace Backend.Services;

public class ImportAndGenerationService : IImportAndGenerationService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ImportAndGenerationService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<string> LoadDistinctsFromFile(string path)
    {
        if (File.Exists(path)) 
        {
            var localVotingDistricts = new Dictionary<string,VotingDistrict>();
            using var sr = new StreamReader(path);
            await sr.ReadLineAsync();
            
            while (await sr.ReadLineAsync() is { } line)
            {
                var items = line.Split(';').Select(x => x.Trim('"')).ToArray();
                
                var districtKey = $"{items[4]}_{items[5]}_{items[6]}";
                var buildingData = BuildingInputClearer(items[13]);
                if (localVotingDistricts.TryGetValue(districtKey, out var dbInstance))
                {
                    dbInstance.VoterAddresses.Add(new VoterAddress
                    {
                        ZipCode = items[10],
                        StreetName = items[11],
                        StreetType = items[12],
                        CityName = items[4],
                        HouseNumberStart = buildingData.start,
                        HouseNumberEnd = buildingData.end,
                        Building = items[14],
                        Staircase = items[15]
                    });
                }
                else
                {
                    var votingDistrict = new VotingDistrict
                    {
                        CountyCode = items[0],
                        CountyName = items[1],
                        OEVK = int.Parse(items[2]),
                        CityCode = items[3],
                        CityName = items[4],
                        TEVK = items[5],
                        PollingStationNumber = int.Parse(items[6]),
                        PollingStationAddress = items[7]
                    };
                    votingDistrict.VoterAddresses.Add(new VoterAddress
                    {
                        ZipCode = items[10],
                        CityName = items[4],
                        StreetName = items[11],
                        StreetType = items[12],
                        HouseNumberStart = buildingData.start,
                        HouseNumberEnd = buildingData.end,
                        Building = items[14],
                        Staircase = items[15]
                    });
                    localVotingDistricts.Add(districtKey, votingDistrict);
                }
            }
            await _context.BulkInsertAsync(localVotingDistricts.Values);
            
            var allVoterAddresses = new List<VoterAddress>();

            foreach (var district in localVotingDistricts.Values)
            {
                foreach (var address in district.VoterAddresses)
                {
                    address.VotingDistrictId = district.Id; 
                    allVoterAddresses.Add(address);
                }
            }
            await _context.BulkInsertAsync(allVoterAddresses);
        }
        else
        {
            throw new Exception("File not found");
        }
        
        return "Succesfully parsed";
    }
    
    private static (int start, int end) BuildingInputClearer(string _HouseNumber)
    {
        var numberPart = _HouseNumber.Split('/')[0];
        var intervalNumbers = numberPart.Split('-');
        int start, end;
        
        if (intervalNumbers.Length > 1)
        {
            string startStr = CleanString(intervalNumbers[0]); 
            string endStr = CleanString(intervalNumbers[1]);  

            int.TryParse(startStr, out start);
            int.TryParse(endStr, out end);    

            if (end == 0 && start != 0)
            {
                end = start;
            }
        }
        else
        {
            string startStr = CleanString(intervalNumbers[0]); 

            int.TryParse(startStr, out start); 
            end = start; 
        }
    
        return (start, end);
        
        static string CleanString(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }

    public async Task<string> WhereAmI(VoterInputDto address)
    {
        var temp = await _context.VoterAddresses.Include(f=>f.VotingDistrict).FirstOrDefaultAsync(x =>
            x.Building == address.Building && x.HouseNumberStart <= address.HouseNumber && x.HouseNumberEnd >= address.HouseNumber  && x.ZipCode == address.ZipCode &&
            x.StreetName == address.StreetName && x.StreetType == address.StreetType) ?? throw new Exception("Address not found");
        return $"{temp.VotingDistrict.CityName}, {temp.VotingDistrict.CountyName}, {temp.VotingDistrict.PollingStationAddress}, {temp.VotingDistrict.PollingStationNumber}";
    }

    public async Task<string> LoadVoterCount(string path)
    {
        if (File.Exists(path))
        {
            var votingDistricts = await _context.VotingDistricts.ToDictionaryAsync(c => (c.CountyName.ToLower(),c.OEVK,c.CityName.ToLower(),c.PollingStationNumber.ToString()));
            foreach (var district in votingDistricts.Values)
            {
                district.EligibleVoterCount = Random.Shared.Next(20, 1000);
            }
            
            using var sr = new StreamReader(path);
            await sr.ReadLineAsync();
            
            
            while (await sr.ReadLineAsync() is { } line)
            {
                var row = line.Split(';').Select(x => x.Trim('"')).ToArray();
                if (row[2] != "Egyéni")
                {
                    continue;
                }
                var districtFormed = row[9].Split('.')[0].ToLower();
                
                if (!int.TryParse(row[5], out int oevk) || !int.TryParse(row[11], out int eligibleVoters) || !int.TryParse(row[10], out int pollingStationNumber))
                {
                   Console.WriteLine($" Hibás szám formátum. Sor: {line}");
                   continue; 
                }
                
                var key = (CountyName: row[4].ToLower(), OEVK: oevk, CityName: districtFormed, pollingStationNumber: pollingStationNumber.ToString());

                if (votingDistricts.TryGetValue(key, out var districtToUpdate))
                {
                    districtToUpdate.EligibleVoterCount = eligibleVoters;
                    
                }
                else
                {
                    Console.WriteLine($" ADATBÁZIS ELTÉRÉS (Kulcs nem található). Kulcs: {key} Sor: {line}");
                }
            }
            
            await _context.BulkUpdateAsync(votingDistricts.Values);
        }
        else
        {
            throw new Exception("File not found");
        }
        
        return "Succesfully parsed";
    }


    public async Task<string> GenerateTokensForDistricts()
    {
        var localDistrict = await _context.VotingDistricts.ToListAsync() ?? throw new Exception("No districts found");
        const int batchSize = 5000;
        var counter = 0;

        var tokenBatch = new List<VotingTokens>(batchSize);
        
        foreach (var district in localDistrict)
        {
            for (var i = 0; i < district.EligibleVoterCount; i++)
            {
                tokenBatch.Add(new VotingTokens
                {
                    VotingDistrictId = district.Id
                });

                if (tokenBatch.Count < batchSize) continue;
                await _context.BulkInsertAsync(tokenBatch);
                counter += tokenBatch.Count;
                tokenBatch.Clear();
            }
        }

        if (tokenBatch.Count > 0)
        {
            await _context.BulkInsertAsync(tokenBatch);
            counter += tokenBatch.Count;
        }
        
        return $"Succesfully generated {counter} token";
    }
    
    
}