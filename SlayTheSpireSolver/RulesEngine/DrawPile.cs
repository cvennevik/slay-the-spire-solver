using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DrawPile
{
    public IReadOnlyList<ICard> Cards { get; }

    public DrawPile(params ICard[] cards)
    {
        Cards = cards;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DrawPile otherDrawPile) return false;
        if (otherDrawPile.Cards.Count != Cards.Count) return false;
        var orderedCards = Cards.OrderBy(x => x.ToString());
        var orderedOtherCards = otherDrawPile.Cards.OrderBy(x => x.ToString());
        return orderedCards.SequenceEqual(orderedOtherCards);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"{nameof(DrawPile)}: [{string.Join(",", Cards)}]";
    }
}
