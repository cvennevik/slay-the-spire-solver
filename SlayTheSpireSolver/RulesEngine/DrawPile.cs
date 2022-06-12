using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DrawPile : CardCollection<DrawPile>
{
    public DrawPile(params Card[] cards) : base(cards)
    {
    }

    protected override DrawPile CreateNew(params Card[] cards)
    {
        return new DrawPile(cards);
    }

    public override string ToString()
    {
        return $"{nameof(DrawPile)}: [{string.Join(",", Cards)}]";
    }
}
