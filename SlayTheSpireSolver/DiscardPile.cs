using SlayTheSpireSolver.Cards;

namespace SlayTheSpireSolver;

public class DiscardPile
{
    public IReadOnlyCollection<ICard> Cards { get; }

    public DiscardPile(params ICard[] cards)
    {
        Cards = cards;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DiscardPile otherDiscardPile) return false;
        if (otherDiscardPile.Cards.Count != Cards.Count) return false;
        var orderedCards = Cards.OrderBy(x => x.ToString());
        var orderedOtherCards = otherDiscardPile.Cards.OrderBy(x => x.ToString());
        return orderedCards.SequenceEqual(orderedOtherCards);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"{nameof(DiscardPile)}: [{string.Join(",", Cards)}]";
    }
}
