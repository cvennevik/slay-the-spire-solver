using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    public IReadOnlyCollection<Card> Cards { get; }

    protected CardCollection(params Card[] cards)
    {
        Cards = cards.OrderBy(card => card.ToString()).ToArray();
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

    public static bool operator ==(CardCollection<T> a, CardCollection<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(CardCollection<T> a, CardCollection<T> b)
    {
        return !a.Equals(b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not T otherCardCollection) return false;
        return otherCardCollection.Cards.Count == Cards.Count && Cards.SequenceEqual(otherCardCollection.Cards);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(",", Cards)}]";
    }
}