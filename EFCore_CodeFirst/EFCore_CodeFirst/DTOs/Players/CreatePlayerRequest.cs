using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EFCore_CodeFirst.DTOs.PlayerInstrument;

namespace EFCore_CodeFirst.DTOs.Players
{
    public class CreatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        public List<CreatePlayerInstrumentRequest> PlayerInstruments { get; set; }
    }
}
