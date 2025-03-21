using Microsoft.EntityFrameworkCore;
using TransactionsTestApi.Data;
using TransactionsTestApi.DTOs;
using TransactionsTestApi.Entities;
using TransactionsTestApi.Services.Interfaces;

namespace TransactionsTestApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UsersReport> GetUsersReportAsync()
    {
        var usersSummary = await GetUserSummariesAsync();

        var topCategories = await GetTopCategoriesAsync(3);

        var mostSpent = usersSummary.MinBy(x => x.TotalExpense);

        var highestSpender = new Spender();

        if (mostSpent != null)
        {
            highestSpender.UserId = mostSpent.UserId;
            highestSpender.TotalExpense = mostSpent.TotalExpense;
        }

        return new UsersReport
        {
            UsersSummary = usersSummary,
            TopCategories = topCategories,
            HighestSpender = highestSpender
        };
    }

    private async Task<IEnumerable<UserSummary>> GetUserSummariesAsync()
    {
        return await _dbContext.Set<Transaction>()
            .AsNoTracking()
            .GroupBy(x => x.UserId)
            .Select(g => new UserSummary
            {
                UserId = g.Key,
                TotalIncome = g.Where(x => x.Amount > 0).Sum(x => x.Amount),
                TotalExpense = g.Where(x => x.Amount < 0).Sum(x => x.Amount)
            }).ToListAsync();
    }

    private async Task<IEnumerable<Category>> GetTopCategoriesAsync(int topNumber)
    {
        return await _dbContext.Set<Transaction>()
            .AsNoTracking()
            .GroupBy(x => x.Category)
            .Select(g => new Category
            {
                Name = g.Key,
                TransactionCount = g.Count()
            })
            .OrderByDescending(x => x.TransactionCount)
            .Take(topNumber)
            .ToListAsync();
    }
}
