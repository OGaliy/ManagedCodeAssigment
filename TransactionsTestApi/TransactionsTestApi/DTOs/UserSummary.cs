using System.Text.Json.Serialization;

namespace TransactionsTestApi.DTOs;

public class UserSummary
{
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    [JsonPropertyName("total_income")]
    public decimal TotalIncome { get; set; }

    [JsonPropertyName("total_expense")]
    public decimal TotalExpense { get; set; }
}
