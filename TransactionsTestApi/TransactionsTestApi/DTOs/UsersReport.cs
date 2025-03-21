using System.Text.Json.Serialization;

namespace TransactionsTestApi.DTOs;

public class UsersReport
{
    [JsonPropertyName("users_summary")]
    public IEnumerable<UserSummary> UsersSummary { get; set; } = null!;

    [JsonPropertyName("top_categories")]
    public IEnumerable<Category> TopCategories { get; set; } = null!;

    [JsonPropertyName("highest_spender")]
    public Spender HighestSpender { get; set; } = null!;
}
