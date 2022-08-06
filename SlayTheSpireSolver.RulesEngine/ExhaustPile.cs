using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class ExhaustPile : CardCollection<ExhaustPile>
{
    public ExhaustPile() : this(Array.Empty<Card>())
    {
    }

    public ExhaustPile(params Card[] cards) : base(cards)
    {
    }

    public override ExhaustPile CreateNew(params Card[] cards)
    {
        return new ExhaustPile(cards);
    }
}

[TestFixture]
internal class ExhaustPileTests : CardCollectionTests<ExhaustPile>
{
}