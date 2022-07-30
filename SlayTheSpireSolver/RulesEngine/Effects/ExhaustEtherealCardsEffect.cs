using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustEtherealCardsEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        var etherealCards = gameState.Hand.Cards.Where(x => x.IsEthereal);
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
        var effect = new ExhaustEtherealCardsEffect();
        var result = effect.Resolve(gameState);
        Assert.AreEqual(gameState, result.Single().GameState);
    }

    [Test]
    public void ExhaustsEtherealCardInHand()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new AscendersBane())
        };
        var effect = new ExhaustEtherealCardsEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = gameState with
        {
            EffectStack = new EffectStack(new ExhaustCardEffect(new AscendersBane()))
        };
    }

    [Test]
    public void ExhaustsAllEtherealCardsInHand()
    {
        // TODO
    }
}