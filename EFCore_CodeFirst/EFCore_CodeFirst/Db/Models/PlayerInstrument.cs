namespace EFCore_CodeFirst.Db.Models
{
    public class PlayerInstrument
    {
        public int PlayerInstrumentId { get; set; }
        public int PlayerId { get; set; }
        public int InstrumentTypeId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }
}
