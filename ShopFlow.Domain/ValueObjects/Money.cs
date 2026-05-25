namespace ShopFlow.Domain.ValueObjects;

public class Money
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }

    public Money(decimal Amount, string Currency)
    {
       
        if (string.IsNullOrEmpty(Currency))
        {
            throw new ArgumentException("Moeda não pode ser vazia.");
        }
        if (Amount < 0)
        {
            throw new ArgumentException("Valor não pode ser negativo.");
        }

        this.Amount = Amount;
        this.Currency = Currency.ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (Currency != other.Currency)
        {
            throw new ArgumentException("Não é possivel somar moedas diferentes.");
        }

        return new Money(Amount + other.Amount, Currency);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money other) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

}