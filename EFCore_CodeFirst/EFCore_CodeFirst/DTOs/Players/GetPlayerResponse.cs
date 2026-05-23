namespace EFCore_CodeFirst.DTOs.Players
{
    public class GetPlayerResponse
    {
        public int PlayerId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public int InstrumentSubmittedCount { get; set; }
    }
}
