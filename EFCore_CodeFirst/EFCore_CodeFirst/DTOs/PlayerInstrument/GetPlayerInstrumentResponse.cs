namespace EFCore_CodeFirst.DTOs.PlayerInstrument
{
    public class GetPlayerInstrumentResponse
    {
        public int PlayerInstrumentId { get; set; }

        public int InstrumentTypeId { get; set; }

        public string InstrumentTypeName { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;
    }
}
