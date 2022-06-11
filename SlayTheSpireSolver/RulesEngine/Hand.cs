using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class Hand : CardCollection<Hand>
{
    public Hand(params ICard[] cards) : base(cards)
    {
    }

    protected override Hand CreateNew(params ICard[] cards)
    {
        return new Hand(cards);
    }

    public override string ToString()
    {
        return $"{nameof(Hand)}: [{string.Join(",", Cards)}]";
    }
}
