using System.Text.Json.Serialization;

namespace TransactionsTestApi.DTOs;

public class Spender
{
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    [JsonPropertyName("total_expense")]
    public decimal TotalExpense { get; set; }
}
