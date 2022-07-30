using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustEtherealCardsEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        throw new NotImplementedException();
    }
}

[TestFixture]
internal class ExhaustEtherealCardsEffectTests
{
    [Test]
    public void DoesNothingWhenHandEmpty()
    {
        var gameState = new GameState();
        var effect = new ExhaustEtherealCardsEffect();
    }
}