using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards.Defend;

[TestFixture]
public class DefendCardTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new DefendCard()),
            Turn = new Turn(1)
        };
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DefendCard(), new DefendCard());
    }

    [Test]
    public void NoLegalActionsWhenNoDefendCardInHand()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand() };
        var legalActions = PlayCardAction.GetLegalActions(gameState, new DefendCard());
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnemy()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        var legalActions = PlayCardAction.GetLegalActions(gameState, new DefendCard());
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenDefeated()
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(0) };
        var legalActions = PlayCardAction.GetLegalActions(gameState, new DefendCard());
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        var gameState = CreateBasicGameState() with { Energy = new Energy(0) };
        var legalActions = PlayCardAction.GetLegalActions(gameState, new DefendCard());
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void OneLegalActionWhenOneEnemy()
    {
        var gameState = CreateBasicGameState();
        var legalActions = PlayCardAction.GetLegalActions(gameState, new DefendCard());
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new PlayCardAction(gameState, new DefendCard()), legalActions.First());
    }
}
