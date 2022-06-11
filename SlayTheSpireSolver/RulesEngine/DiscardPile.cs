using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DiscardPile : CardCollection<DiscardPile>
{
    public DiscardPile(params ICard[] cards) : base(cards)
    {
    }

    protected override DiscardPile CreateNew(params ICard[] cards)
    {
        return new DiscardPile(cards);
    }

    public override string ToString()
    {
        return $"{nameof(DiscardPile)}: [{string.Join(",", Cards)}]";
    }
}
