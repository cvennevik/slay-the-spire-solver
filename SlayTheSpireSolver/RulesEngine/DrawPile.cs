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
        return Cards.SequenceEqual(otherDrawPile.Cards);
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
