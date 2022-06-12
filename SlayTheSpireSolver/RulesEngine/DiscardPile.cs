using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DiscardPile : CardCollection<DiscardPile>
{
    public DiscardPile(params Card[] cards) : base(cards)
    {
    }

    protected override DiscardPile CreateNew(params Card[] cards)
    {
        return new DiscardPile(cards);
    }

    public override string ToString()
    {
        return $"{nameof(DiscardPile)}: [{string.Join(",", Cards)}]";
    }
}
