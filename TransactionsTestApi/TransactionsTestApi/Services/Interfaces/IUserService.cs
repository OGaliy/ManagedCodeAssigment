using TransactionsTestApi.DTOs;

namespace TransactionsTestApi.Services.Interfaces;

public interface IUserService
{
    public Task<UsersReport> GetUsersReportAsync();
}
