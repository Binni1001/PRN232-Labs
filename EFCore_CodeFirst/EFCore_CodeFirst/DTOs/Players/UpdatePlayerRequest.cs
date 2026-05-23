using System.ComponentModel.DataAnnotations;

namespace EFCore_CodeFirst.DTOs.Players
{
    public class UpdatePlayerRequest
    {
        [Required]
        public string NickName { get; set; } = string.Empty;
    }
}
