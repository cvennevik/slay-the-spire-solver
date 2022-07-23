using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DrawPile : CardCollection<DrawPile>
{
    public DrawPile() : this(Array.Empty<Card>())
    {
    }

    public DrawPile(params Card[] cards) : base(cards)
    {
    }

    public override DrawPile CreateNew(params Card[] cards)
    {
        return new DrawPile(cards);
    }
}

[TestFixture]
internal class DrawPileTests : CardCollectionTests<DrawPile>
{
}