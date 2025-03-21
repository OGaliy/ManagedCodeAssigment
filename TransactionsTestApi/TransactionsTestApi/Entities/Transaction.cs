namespace TransactionsTestApi.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Merchant { get; set; } = null!;
}
