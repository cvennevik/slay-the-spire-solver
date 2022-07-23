using System.Collections.Concurrent;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    private static readonly ConcurrentDictionary<(T, Card), T> Cache = new();

    public IReadOnlyCollection<Card> Cards { get; }
    private readonly int _hashCode;

    protected CardCollection(params Card[] cards)
    {
        Array.Sort(cards);
        Cards = cards;
        _hashCode = Cards.Sum(x => x.GetHashCode());
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
        var cardsCopy = Cards.ToList();
        var cardFound = cardsCopy.Remove(card);
        if (!cardFound) throw new ArgumentException($"CardCollection does not contain {card}");
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
        return obj is T otherCardCollection &&
               _hashCode == otherCardCollection._hashCode &&
               Cards.SequenceEqual(otherCardCollection.Cards);
    }

    public override int GetHashCode()
    {
        return _hashCode;
    }

    public override string ToString()
    {
        return $"[{string.Join(",", Cards)}]";
    }
}