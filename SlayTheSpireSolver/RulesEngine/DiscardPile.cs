using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DiscardPile : CardCollection<DiscardPile>
{
    public DiscardPile() : this(Array.Empty<Card>())
    {
    }

    public DiscardPile(params Card[] cards) : base(cards)
    {
    }

    public override DiscardPile CreateNew(params Card[] cards)
    {
        return new DiscardPile(cards);
    }
}

[TestFixture]
internal class DiscardPileTests : CardCollectionTests<DiscardPile>
{
}