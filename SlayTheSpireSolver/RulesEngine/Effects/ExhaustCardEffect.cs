using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustCardEffect(Card TargetCard) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with
        {
            Hand = gameState.Hand.Remove(TargetCard),
            ExhaustPile = gameState.ExhaustPile.Add(TargetCard)
        };
    }
}

[TestFixture]
internal class ExhaustCardEffectTests
{
    [Test]
    public void ExhaustsOneOfTargetCard()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Strike(), new Defend())
        };
        var effect = new ExhaustCardEffect(new Strike());
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            ExhaustPile = new ExhaustPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }

    [Test]
    public void AddsAdditionalCopyToExhaustPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            ExhaustPile = new ExhaustPile(new Strike())
        };
        var effect = new ExhaustCardEffect(new Strike());
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            ExhaustPile = new ExhaustPile(new Strike(), new Strike())
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }

    [Test]
    public void DoesNothingWhenCardNotInHand()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Defend())
        };
        var effect = new ExhaustCardEffect(new Strike());
    }
}