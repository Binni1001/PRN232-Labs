using System.ComponentModel.DataAnnotations;

namespace EFCore_CodeFirst.DTOs.PlayerInstrument
{
    public class CreatePlayerInstrumentRequest
    {
        [Required]
        public int InstrumentTypeId { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string Level { get; set; }
    }
}
