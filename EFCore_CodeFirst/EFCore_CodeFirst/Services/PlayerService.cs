using EFCore_CodeFirst.Db;
using EFCore_CodeFirst.Db.Models;
using EFCore_CodeFirst.DTOs;
using EFCore_CodeFirst.DTOs.PlayerInstrument;
using EFCore_CodeFirst.DTOs.Players;
using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirst.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly CodeFirstDemoContext _dbContext;

        public PlayerService(CodeFirstDemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePlayerAsync(CreatePlayerRequest playerRequest)
        {
            var player = new Player
            {
                NickName = playerRequest.NickName,
                JoinedDate = DateTime.Now,
                Instruments = playerRequest.PlayerInstruments.Select(x => new PlayerInstrument
                {
                    InstrumentTypeId = x.InstrumentTypeId,
                    ModelName = x.ModelName,
                    Level = x.Level
                }).ToList()
            };

            _dbContext.Players.Add(player);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest)
        {
            var player = await _dbContext.Players.FindAsync(id);

            if (player == null)
                return false;

            player.NickName = playerRequest.NickName;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _dbContext.Players.FindAsync(id);

            if (player == null)
                return false;

            _dbContext.Players.Remove(player);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<GetPlayerDetailResponse> GetPlayerDetailAsync(int id)
        {
            var player = await _dbContext.Players
                .Include(p => p.Instruments)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null)
                return null;

            return new GetPlayerDetailResponse
            {
                NickName = player.NickName,
                JoinedDate = player.JoinedDate,

                PlayerInstruments = player.Instruments.Select(i => new GetPlayerInstrumentResponse
                {
                    InstrumentTypeId = i.InstrumentTypeId,
                    ModelName = i.ModelName,
                    Level = i.Level
                }).ToList()
            };
        }

        public async Task<PagedResponse<GetPlayerResponse>> GetPlayersAsync(UrlQueryParameters parameters)
        {
            var query = _dbContext.Players
                .Include(p => p.Instruments)
                .AsQueryable();

            var totalRecords = await query.CountAsync();

            var players = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var response = players.Select(player => new GetPlayerResponse
            {
                PlayerId = player.PlayerId,
                NickName = player.NickName,
                JoinedDate = player.JoinedDate,
                InstrumentSubmittedCount = player.Instruments.Count
            }).ToList();

            return new PagedResponse<GetPlayerResponse>
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalRecords = totalRecords,
                Data = response
            };
        }
    
    }
}
