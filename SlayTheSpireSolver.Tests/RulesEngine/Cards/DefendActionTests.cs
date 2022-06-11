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
