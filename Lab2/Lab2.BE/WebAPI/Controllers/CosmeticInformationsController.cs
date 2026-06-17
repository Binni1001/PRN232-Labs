using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace WebAPI.Controllers;

[ApiController]
public class CosmeticInformationsController : ODataController
{
    private readonly ICosmeticInformationService _service;

    public CosmeticInformationsController(ICosmeticInformationService service)
    {
        _service = service;
    }

    [EnableQuery]
    [Authorize(Policy = "AdminOrStaffOrMember")]
    [HttpGet("/api/CosmeticInformations")]
    public async Task<ActionResult<IEnumerable<CosmeticInformation>>> GetCosmeticInformations()
    {
        var result = await _service.GetAllCosmetics();
        return Ok(result);
    }

    [Authorize(Policy = "AdminOrStaffOrMember")]
    [HttpGet("/api/CosmeticInformations/{id}")]
    public async Task<ActionResult<CosmeticInformation>> GetCosmeticInformationById(string id)
    {
        var result = await _service.GetOne(id);

        if (result == null)
            return NotFound("Cosmetic not found");

        return Ok(result);
    }

    [Authorize(Policy = "AdminOrStaffOrMember")]
    [HttpGet("/api/CosmeticCategories")]
    public async Task<ActionResult<List<CosmeticCategory>>> GetCategories()
    {
        var result = await _service.GetAllCategories();
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("/api/CosmeticInformations")]
    public async Task<ActionResult<CosmeticInformation>> AddCosmeticInformation(
        [FromBody] CosmeticInformation cosmeticInformation)
    {
        var result = await _service.Add(cosmeticInformation);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("/api/CosmeticInformations/{id}")]
    public async Task<ActionResult<CosmeticInformation>> UpdateCosmeticInformation(
        string id,
        [FromBody] CosmeticInformation cosmeticInformation)
    {
        cosmeticInformation.CosmeticId = id;

        var result = await _service.Update(cosmeticInformation);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("/api/CosmeticInformations/{id}")]
    public async Task<ActionResult<CosmeticInformation>> DeleteCosmeticInformation(string id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}