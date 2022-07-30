using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class ExhaustPile : CardCollection<ExhaustPile>
{
    private ExhaustPile(params Card[] cards) : base(cards)
    {
    }

    public override ExhaustPile CreateNew(params Card[] cards)
    {
        return new ExhaustPile(cards);
    }
}