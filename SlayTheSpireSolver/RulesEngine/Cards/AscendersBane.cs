using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record AscendersBane : Card
{
    public override bool IsEthereal => true;
    public override Energy Cost => 0;
    protected override string Name => "Ascender's Bane";

    public override IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState)
    {
        return Array.Empty<PlayCardAction>();
    }
}

[TestFixture]
internal class AscendersBaneTests
{
}