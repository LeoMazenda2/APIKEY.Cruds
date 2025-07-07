using APIKEY.Crudes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIKEY.Crudes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _service;
    public ApiKeyController(IApiKeyService service) => _service = service;

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var apiKeys = await _service.GetAllAsync();
        return Ok(apiKeys);
    }

    [HttpPost]
    public async Task<ActionResult<CreateApiKeyResponse>> Post(CreateApiKeyRequest req)
    {
        var dto = await _service.GenerateAsync(req.LicenseXd, req.Email, req.ValidUntil);
        return Ok(new CreateApiKeyResponse(dto.LicenseXd, dto.apiKey, dto.Token, dto.ValidUntil));
    }

    public record CreateApiKeyRequest(string LicenseXd, string Email, DateTimeOffset ValidUntil);
    public record CreateApiKeyResponse(string LicenseXd, string apiKey, string Token, DateTimeOffset ValidUntil);

}
