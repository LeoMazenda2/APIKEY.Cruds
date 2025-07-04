using APIKEY.Crudes.Models;
using APIKEY.Crudes.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIKEY.Crudes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiKeyController : ControllerBase
{
    private readonly ApiKeyService _service;

    public ApiKeyController(ApiKeyService service)
    {
        _service = service;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] ApiKeyRequest request)
    {
        var apiKey = await _service.GenerateApiKeyAsync(
            request.LicenseKey,
            request.CreatedBy,
            request.Remarks,
            request.ValidUntil
        );
        return Ok(new { ApiKey = apiKey });
    }
}
