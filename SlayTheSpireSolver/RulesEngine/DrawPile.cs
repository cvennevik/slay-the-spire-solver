using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DrawPile : CardCollection<DrawPile>
{
    public DrawPile(params ICard[] cards) : base(cards)
    {
    }

    protected override DrawPile CreateNew(params ICard[] cards)
    {
        return new DrawPile(cards);
    }

    public override string ToString()
    {
        return $"{nameof(DrawPile)}: [{string.Join(",", Cards)}]";
    }
}
