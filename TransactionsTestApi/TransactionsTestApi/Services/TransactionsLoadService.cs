using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TransactionsTestApi.Data;
using TransactionsTestApi.Entities;
using TransactionsTestApi.Services.Interfaces;

namespace TransactionsTestApi.Services;

public class TransactionsLoadService : ITransactionsService
{
    private readonly AppDbContext _dbContext;
    private const int BatchSize = 10000;

    public TransactionsLoadService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LoadTransactionsAsync(IFormFile formFile)
    {
        using var stream = formFile.OpenReadStream();
        using var reader = new StreamReader(stream); 
        
        var batch = new List<Transaction>(BatchSize);
        int counter = 0;

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("TransactionId"))
            {
                continue;
            }

            var values = line.Split(',');

            var transaction = CreateTransaction(values);

            batch.Add(transaction);
            counter++;

            if (counter % BatchSize == 0)
            {
                await _dbContext.BulkInsertAsync(batch);
                batch.Clear();
            }
        }

        if (batch.Count > 0)
        {
            await _dbContext.BulkInsertAsync(batch);
        }
    }

    public async Task DeleteAllTransactionAsync()
    {
        await _dbContext.Set<Transaction>().ExecuteDeleteAsync();
    }

    private static Transaction CreateTransaction(string[] values)
    {
        return new()
        {
            TransactionId = Guid.Parse(values[0]),
            UserId = Guid.Parse(values[1]),
            Date = DateTime.Parse(values[2]),
            Amount = decimal.Parse(values[3]),
            Category = values[4],
            Description = values[5],
            Merchant = values[6]
        };
    }
}
