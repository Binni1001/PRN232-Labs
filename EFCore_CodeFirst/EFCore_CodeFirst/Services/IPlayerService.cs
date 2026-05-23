using EFCore_CodeFirst.DTOs;
using EFCore_CodeFirst.DTOs.PlayerInstrument;
using EFCore_CodeFirst.DTOs.Players;

namespace EFCore_CodeFirst.Services
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(
        CreatePlayerRequest playerRequest);

        Task<bool> UpdatePlayerAsync(
            int id,
            UpdatePlayerRequest playerRequest);

        Task<bool> DeletePlayerAsync(int id);

        Task<GetPlayerDetailResponse>
            GetPlayerDetailAsync(int id);

        Task<PagedResponse<GetPlayerResponse>>
            GetPlayersAsync(
                UrlQueryParameters parameters);
    }
}
