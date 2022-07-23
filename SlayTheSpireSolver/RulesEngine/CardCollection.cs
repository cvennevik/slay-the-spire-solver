using System.Collections.Concurrent;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    private static readonly ConcurrentDictionary<(T, Card), T> AddCache = new();
    private static readonly ConcurrentDictionary<(T, Card), T> RemoveCache = new();

    public IReadOnlyCollection<Card> Cards { get; }
    private readonly int _hashCode;

    protected CardCollection(params Card[] cards)
    {
        Array.Sort(cards);
        Cards = cards;
        _hashCode = Cards.Sum(x => x.GetHashCode());
    }

    public abstract T CreateNew(params Card[] cards);

    public bool Contains(Card card)
    {
        return Cards.Contains(card);
    }

    public T Add(Card card)
    {
        return AddCache.GetOrAdd(((T)this, card), key =>
        {
            var (cardCollection, innerCard) = key;
            return CreateNew(cardCollection.Cards.Append(innerCard).ToArray());
        });
    }

    public T Remove(Card card)
    {
        return RemoveCache.GetOrAdd(((T)this, card), key =>
        {
            var (cardCollection, innerCard) = key;
            var cardsCopy = cardCollection.Cards.ToList();
            var cardFound = cardsCopy.Remove(innerCard);
            if (!cardFound) throw new ArgumentException($"CardCollection does not contain {card}");
            return CreateNew(cardsCopy.ToArray());
        });
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

// TODO: Add common CardCollection tests

[TestFixture]
internal abstract class CardCollectionTests<T> where T : CardCollection<T>, new()
{
    private readonly T _type = new();

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(_type.CreateNew(), _type.CreateNew());
        Assert.AreNotEqual(_type.CreateNew(), _type.CreateNew(new Strike()));
        Assert.AreNotEqual(_type.CreateNew(new Defend()), _type.CreateNew(new Strike()));
        Assert.AreEqual(_type.CreateNew(new Strike()), _type.CreateNew(new Strike()));
    }
}