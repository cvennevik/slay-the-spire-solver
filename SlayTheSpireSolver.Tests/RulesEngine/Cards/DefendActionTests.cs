using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class DefendActionTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new Defend()),
            DiscardPile = new DiscardPile(),
        };
    }

    [Test]
    public void ActionsWithSameGameStatesAreEqual()
    {
        var gameState = CreateBasicGameState();
        Assert.AreEqual(new PlayCardAction(gameState, new Defend()), new PlayCardAction(gameState, new Defend()));
    }

    [Test]
    public void ActionsWithDifferentGameStatesAreNotEqual()
    {
        var gameState1 = CreateBasicGameState();
        var gameState2 = CreateBasicGameState() with { Turn = new Turn(2) };
        Assert.AreNotEqual(new PlayCardAction(gameState1, new Defend()),
            new PlayCardAction(gameState2, new Defend()));
    }

    [Test]
    [TestCase(0, 5)]
    [TestCase(2, 7)]
    public void AddsPlayerArmorAndRemovesDefendCard(int initialAmountOfArmor, int expectedAmountOfArmor)
    {
        var gameState = new GameState()
        {
            PlayerArmor = new Armor(initialAmountOfArmor),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new Defend()),
            DiscardPile = new DiscardPile()
        };
        var defendAction = new PlayCardAction(gameState, new Defend());
        var resolvedStates = defendAction.ResolveToPossibleStates();
        var expectedState = new GameState()
        {
            PlayerArmor = new Armor(expectedAmountOfArmor),
            Energy = new Energy(2),
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Defend())
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }
}
