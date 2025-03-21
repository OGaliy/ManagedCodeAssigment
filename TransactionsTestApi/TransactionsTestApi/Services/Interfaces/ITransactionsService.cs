namespace TransactionsTestApi.Services.Interfaces;

public interface ITransactionsService
{
    public Task LoadTransactionsAsync(IFormFile formFile);

    public Task DeleteAllTransactionAsync();
}
