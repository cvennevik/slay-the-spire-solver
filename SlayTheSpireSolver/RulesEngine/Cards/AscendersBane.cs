using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record AscendersBane : Card
{
    public override Energy Cost => 0;
    public override string Name => "Ascender's Bane";

    public override IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState)
    {
        return Array.Empty<PlayCardAction>();
    }
}

[TestFixture]
internal class AscendersBaneTests
{
}