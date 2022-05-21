using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public abstract class CardCollection<T> where T : CardCollection<T>
{
    public IReadOnlyCollection<ICard> Cards { get; }

    protected CardCollection(params ICard[] cards)
    {
        Cards = cards;
    }

    protected abstract T CreateNew(params ICard[] cards);

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

    public bool Contains(ICard card)
    {
        return Cards.Contains(card);
    }

    public T Remove(ICard card)
    {
        if (!Cards.Contains(card)) throw new ArgumentException($"CardCollection does not contain {card}");
        var cardsCopy = Cards.ToList();
        cardsCopy.Remove(card);
        return CreateNew(cardsCopy.ToArray());
    }

    public T Add(ICard card)
    {
        return CreateNew(Cards.Append(card).ToArray());
    }
}