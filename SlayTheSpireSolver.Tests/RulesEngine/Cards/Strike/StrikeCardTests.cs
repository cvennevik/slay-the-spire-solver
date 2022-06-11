using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards.Strike;

[TestFixture]
public class StrikeCardTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new StrikeCard()),
            Turn = new Turn(1)
        };
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new StrikeCard(), new StrikeCard());
    }

    [Test]
    public void NoLegalActionsWhenNoStrikeCardInHand()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand() };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnemy()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenDefeated()
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(0) };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        var gameState = CreateBasicGameState() with { Energy = new Energy(0) };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void OneLegalActionWhenOneEnemy()
    {
        var gameState = CreateBasicGameState();
        var legalActions = new StrikeCard().GetLegalActions(gameState).ToList();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new PlayCardAction(gameState, new StrikeCard()), legalActions.First());
    }
}
