using SlayTheSpireSolver.Cards;

namespace SlayTheSpireSolver;

public class Hand
{
    public IReadOnlyCollection<ICard> Cards { get; init; } = Array.Empty<ICard>();

    public override bool Equals(object obj)
    {
        var otherHand = obj as Hand;

        if (otherHand == null) return false;
        if (otherHand.Cards.Count != Cards.Count) return false;
        var orderedCards = Cards.OrderBy(x => x.GetType());
        var orderedOtherCards = otherHand.Cards.OrderBy(x => x.GetType());
        return orderedCards.SequenceEqual(orderedOtherCards);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
