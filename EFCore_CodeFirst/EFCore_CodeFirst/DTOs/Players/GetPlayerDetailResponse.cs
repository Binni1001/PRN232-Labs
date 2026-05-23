using EFCore_CodeFirst.DTOs.PlayerInstrument;

namespace EFCore_CodeFirst.DTOs.Players
{
    public class GetPlayerDetailResponse
    {
        public string NickName { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public List<GetPlayerInstrumentResponse> PlayerInstruments { get; set; } = new List<GetPlayerInstrumentResponse>();
    }
}
