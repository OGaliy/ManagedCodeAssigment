using Microsoft.AspNetCore.Mvc;
using TransactionsTestApi.Services.Interfaces;

namespace TransactionsTestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersReportController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersReportController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReportAsync()
    {
        var result = await _userService.GetUsersReportAsync();

        return Ok(result);
    }
}
