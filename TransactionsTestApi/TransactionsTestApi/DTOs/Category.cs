using System.Text.Json.Serialization;

namespace TransactionsTestApi.DTOs;

public class Category
{
    [JsonPropertyName("category")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("transactions_count")]
    public int TransactionCount { get; set; }
}
