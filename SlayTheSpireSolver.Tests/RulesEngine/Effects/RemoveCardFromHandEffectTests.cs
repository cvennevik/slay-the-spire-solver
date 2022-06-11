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
        var gameState = new GameState { Hand = new Hand(new StrikeCard()) };
        var effect = new RemoveCardFromHandEffect(new DefendCard());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(gameState, newGameStates.Single());
    }

    [Test]
    public void RemovesSingleCardFromHand()
    {
        var gameState = new GameState { Hand = new Hand(new StrikeCard()) };
        var effect = new RemoveCardFromHandEffect(new StrikeCard());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(new GameState { Hand = new Hand() }, newGameStates.Single());
    }

    [Test]
    public void RemovesOnlyOneCardWhenMultipleInHand()
    {
        var gameState = new GameState { Hand = new Hand(new StrikeCard(), new StrikeCard()) };
        var effect = new RemoveCardFromHandEffect(new StrikeCard());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(new GameState { Hand = new Hand(new StrikeCard()) }, newGameStates.Single());
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RemoveCardFromHandEffect(new StrikeCard()), new RemoveCardFromHandEffect(new StrikeCard()));
        Assert.AreEqual(new RemoveCardFromHandEffect(new DefendCard()), new RemoveCardFromHandEffect(new DefendCard()));
        Assert.AreNotEqual(new RemoveCardFromHandEffect(new StrikeCard()),
            new RemoveCardFromHandEffect(new DefendCard()));
    }
}