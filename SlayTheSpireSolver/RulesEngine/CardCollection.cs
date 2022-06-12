using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    public IReadOnlyCollection<Card> Cards { get; }

    protected CardCollection(params Card[] cards)
    {
        Cards = cards;
    }

    protected abstract T CreateNew(params Card[] cards);

    public bool Contains(Card card)
    {
        return Cards.Contains(card);
    }

    public T Add(Card card)
    {
        return CreateNew(Cards.Append(card).ToArray());
    }

    public T Remove(Card card)
    {
        if (!Cards.Contains(card)) throw new ArgumentException($"CardCollection does not contain {card}");
        var cardsCopy = Cards.ToList();
        cardsCopy.Remove(card);
        return CreateNew(cardsCopy.ToArray());
    }

    public static bool operator ==(CardCollection<T> a, CardCollection<T> b) => a.Equals(b);
    public static bool operator !=(CardCollection<T> a, CardCollection<T> b) => !a.Equals(b);
    public override bool Equals(object? obj)
    {
        if (obj is not T otherCardCollection) return false;
        if (otherCardCollection.Cards.Count != Cards.Count) return false;
        var orderedCards = Cards.OrderBy(x => x.ToString());
        var orderedOtherCards = otherCardCollection.Cards.OrderBy(x => x.ToString());
        return orderedCards.SequenceEqual(orderedOtherCards);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}