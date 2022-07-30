using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustEtherealCardsEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState;
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
        var result = effect.Resolve(gameState);
        Assert.AreEqual(gameState, result.Single().GameState);
    }

    [Test]
    public void DoesNothingWhenNoEtherealCardsInHand()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend(), new Bash())
        };
    }
}