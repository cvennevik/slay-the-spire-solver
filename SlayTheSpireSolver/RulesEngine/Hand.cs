using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class Hand : CardCollection<Hand>
{
    public Hand() : this(Array.Empty<Card>())
    {
    }

    public Hand(params Card[] cards) : base(cards)
    {
    }

    public override Hand CreateNew(params Card[] cards)
    {
        return new Hand(cards);
    }
}

[TestFixture]
internal class HandTests : CardCollectionTests<Hand>
{
}