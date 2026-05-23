using EFCore_CodeFirst.DTOs;
using EFCore_CodeFirst.DTOs.Players;
using EFCore_CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetPlayersAsync([FromQuery] UrlQueryParameters parameters)
        {
            var result = await _playerService.GetPlayersAsync(parameters);

            return Ok(result);
        }

        // GET DETAIL
        [HttpGet("{id:int}/detail")]
        public async Task<IActionResult> GetPlayerDetailAsync(int id)
        {
            var result = await _playerService.GetPlayerDetailAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> PostPlayerAsync([FromBody] CreatePlayerRequest playerRequest)
        {
            await _playerService.CreatePlayerAsync(playerRequest);

            return Ok(new
            {
                Message = "Player created successfully"
            });
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPlayerAsync(int id, [FromBody] UpdatePlayerRequest playerRequest)
        {
            var updated = await _playerService.UpdatePlayerAsync(id, playerRequest);

            if (!updated)
                return NotFound();

            return Ok(new
            {
                Message = "Player updated successfully"
            });
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayerAsync(int id)
        {
            var deleted = await _playerService.DeletePlayerAsync(id);

            if (!deleted)
                return NotFound();

            return Ok(new
            {
                Message = "Player deleted successfully"
            });
        }
    }
}
