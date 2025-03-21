using Microsoft.AspNetCore.Mvc;
using TransactionsTestApi.Services.Interfaces;

namespace TransactionsTestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        await _transactionsService.LoadTransactionsAsync(file);
        return Ok("Done");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllAsync()
    {
        await _transactionsService.DeleteAllTransactionAsync();

        return NoContent();
    }
}
