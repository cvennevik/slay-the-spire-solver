using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record AscendersBane : Card
{
    public override Energy GetCost()
    {
        return 0;
    }

    public override IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState)
    {
        return Array.Empty<PlayCardAction>();
    }

    protected override string GetName()
    {
        return "Ascender's Bane";
    }
}

[TestFixture]
internal class AscendersBaneTests
{
}