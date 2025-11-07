using AutoMapper;
using Backend.Context;
using Backend.DTOs;
using Backend.Entities;
using Backend.interfaces;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace Backend.Services;

public class VotingDistrictService : IVotingDistrictService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public VotingDistrictService(AppDbContext context, IMapper mapper)
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
                        OEVK = items[2],
                        CityCode = items[3],
                        CityName = items[4],
                        TEVK = items[5],
                        PollingStationNumber = items[6],
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

    
}