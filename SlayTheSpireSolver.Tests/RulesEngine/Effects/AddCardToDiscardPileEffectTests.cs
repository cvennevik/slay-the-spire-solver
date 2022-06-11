using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddCardToDiscardPileEffectTests
{
    [Test]
    public void AddsToEmptyDiscardPile()
    {
        var gameState = new GameState { DiscardPile = new DiscardPile() };
        var effect = new AddCardToDiscardPileEffect(new StrikeCard());
        var newGameStates = effect.ApplyTo(gameState);
        Assert.AreEqual(new GameState { DiscardPile = new DiscardPile(new StrikeCard()) }, newGameStates.Single());
    }

    [Test]
    public void AddsToDiscardPileWithExistingCards()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new StrikeCard(), new DefendCard())
        };
        var effect = new AddCardToDiscardPileEffect(new StrikeCard());
        var newGameStates = effect.ApplyTo(gameState);
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(new StrikeCard(), new DefendCard(), new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameStates.Single());
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AddCardToDiscardPileEffect(new StrikeCard()),
            new AddCardToDiscardPileEffect(new StrikeCard()));
        Assert.AreEqual(new AddCardToDiscardPileEffect(new DefendCard()),
            new AddCardToDiscardPileEffect(new DefendCard()));
        Assert.AreNotEqual(new AddCardToDiscardPileEffect(new StrikeCard()),
            new AddCardToDiscardPileEffect(new DefendCard()));
    }
}