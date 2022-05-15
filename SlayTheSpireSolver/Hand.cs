using SlayTheSpireSolver.Cards;

namespace SlayTheSpireSolver;

public class Hand
{
    public IReadOnlyCollection<ICard> Cards { get; }

    public Hand(params ICard[] cards)
    {
        Cards = cards;
    }

    public bool Contains(ICard card)
    {
        return Cards.Contains(card);
    }

    public Hand Remove(ICard card)
    {
        if (!Contains(card)) throw new ArgumentException($"Hand does not contain card of type {card.GetType}");
        var cardsCopy = Cards.ToList();
        cardsCopy.Remove(card);
        return new Hand(cardsCopy.ToArray());
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Hand otherHand) return false;
        if (otherHand.Cards.Count != Cards.Count) return false;
        var orderedCards = Cards.OrderBy(x => x.ToString());
        var orderedOtherCards = otherHand.Cards.OrderBy(x => x.ToString());
        return orderedCards.SequenceEqual(orderedOtherCards);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"{nameof(Hand)}: [{string.Join(",", Cards)}]";
    }
}
