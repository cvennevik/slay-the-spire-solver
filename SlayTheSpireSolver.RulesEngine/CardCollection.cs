using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    private static readonly Dictionary<(T, Card), T> AddCache = new();
    private static readonly Dictionary<(T, Card), T> RemoveCache = new();

    public IReadOnlyCollection<Card> Cards { get; }
    private readonly int _hashCode;

    protected CardCollection(params Card[] cards)
    {
        Array.Sort(cards);
        Cards = cards;
        _hashCode = Cards.Aggregate(0, HashCode.Combine);
    }

    public abstract T CreateNew(params Card[] cards);

    public bool Contains(Card card)
    {
        return Cards.Contains(card);
    }

    public T Add(Card card)
    {
        if (AddCache.TryGetValue(((T)this, card), out var cachedValue)) return cachedValue;

        var result = CreateNew(Cards.Append(card).ToArray());
        AddCache[((T)this, card)] = result;
        return result;
    }

    public T Remove(Card card)
    {
        if (RemoveCache.TryGetValue(((T)this, card), out var cachedValue)) return cachedValue;

        var cardsCopy = Cards.ToList();
        var cardFound = cardsCopy.Remove(card);
        if (!cardFound) throw new ArgumentException($"CardCollection does not contain {card}");
        var result = CreateNew(cardsCopy.ToArray());
        RemoveCache[((T)this, card)] = result;
        return result;
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
        if (ReferenceEquals(this, obj)) return true;
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
        Assert.AreNotEqual(_type.CreateNew(new Strike()), _type.CreateNew(new Strike(), new Strike()));
        Assert.AreEqual(_type.CreateNew(new Strike(), new Defend()), _type.CreateNew(new Strike(), new Defend()));
        Assert.AreEqual(_type.CreateNew(new Strike(), new Defend()), _type.CreateNew(new Defend(), new Strike()));
    }

    [Test]
    public void ContainsDefend()
    {
        var collection = _type.CreateNew(new Defend());
        Assert.True(collection.Contains(new Defend()));
        Assert.False(collection.Contains(new Strike()));
    }

    [Test]
    public void ContainsStrikeAndDefend()
    {
        var collection = _type.CreateNew(new Defend(), new Strike());
        Assert.True(collection.Contains(new Defend()));
        Assert.True(collection.Contains(new Strike()));
    }

    [Test]
    public void ContainsNothing()
    {
        var collection = _type.CreateNew();
        Assert.False(collection.Contains(new Defend()));
        Assert.False(collection.Contains(new Strike()));
    }

    [Test]
    public void ContainsTwoDefends()
    {
        var collection = _type.CreateNew(new Defend(), new Defend());
        Assert.True(collection.Contains(new Defend()));
        Assert.False(collection.Contains(new Strike()));
    }

    [Test]
    public void AddsStrikeToEmptyCollection()
    {
        var collection = _type.CreateNew().Add(new Strike());
        Assert.AreEqual(_type.CreateNew(new Strike()), collection);
    }

    [Test]
    public void AddsDefendToExistingCollection()
    {
        var collection = _type.CreateNew(new Strike()).Add(new Defend());
        Assert.AreEqual(_type.CreateNew(new Strike(), new Defend()), collection);
    }

    [Test]
    public void RemoveThrowsExceptionWhenCollectionEmpty()
    {
        var collection = _type.CreateNew();
        Assert.Throws<ArgumentException>(() => collection.Remove(new Strike()));
    }

    [Test]
    public void RemovesDefendCard()
    {
        var collection = _type.CreateNew(new Defend());
        var newCollection = collection.Remove(new Defend());
        Assert.AreEqual(_type.CreateNew(), newCollection);
    }

    [Test]
    public void RemovesOnlyOneCopy()
    {
        var collection = _type.CreateNew(new Strike(), new Strike());
        var newCollection = collection.Remove(new Strike());
        Assert.AreEqual(_type.CreateNew(new Strike()), newCollection);
    }

    [Test]
    public void RemovesCorrectCardType()
    {
        var collection = _type.CreateNew(new Strike(), new Defend());
        var newCollection = collection.Remove(new Strike());
        Assert.AreEqual(_type.CreateNew(new Defend()), newCollection);
    }

    [Test]
    public void RemoveThrowsExceptionWhenCardNotInCollection()
    {
        var collection = _type.CreateNew(new Defend());
        Assert.Throws<ArgumentException>(() => collection.Remove(new Strike()));
    }
}