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

        foreach (var uniqueCard in Cards.ToHashSet())
        {
            var cardsInHand = Cards.Count(card => card == uniqueCard);
            var cardsInOtherHand = otherHand.Cards.Count(card => card == uniqueCard);
            if (cardsInHand != cardsInOtherHand) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
