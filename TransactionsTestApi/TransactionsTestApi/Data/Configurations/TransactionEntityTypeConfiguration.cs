using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionsTestApi.Entities;

namespace TransactionsTestApi.Data.Configurations;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(x => x.TransactionId);

        builder.Property(x => x.TransactionId)
            .HasColumnName("transaction_id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.Date)
            .HasColumnName("date");

        builder.Property(x => x.Amount)
            .HasColumnName("amount");

        builder.Property(x => x.Category)
            .HasColumnName("category");

        builder.Property(x => x.Description)
            .HasColumnName("description");

        builder.Property(x => x.Merchant)
            .HasColumnName("merchant");

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.Amount);
    }
}
