using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class RemoveCardFromHandEffectTests
{
    [Test]
    public void DoesNothingWhenCardNotInHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Defend());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(gameState, newGameStates.Single());
    }

    [Test]
    public void RemovesSingleCardFromHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Strike());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(new GameState { Hand = new Hand() }, newGameStates.Single());
    }

    [Test]
    public void RemovesOnlyOneCardWhenMultipleInHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike(), new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Strike());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(new GameState { Hand = new Hand(new Strike()) }, newGameStates.Single());
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RemoveCardFromHandEffect(new Strike()), new RemoveCardFromHandEffect(new Strike()));
        Assert.AreEqual(new RemoveCardFromHandEffect(new Defend()), new RemoveCardFromHandEffect(new Defend()));
        Assert.AreNotEqual(new RemoveCardFromHandEffect(new Strike()),
            new RemoveCardFromHandEffect(new Defend()));
    }
}