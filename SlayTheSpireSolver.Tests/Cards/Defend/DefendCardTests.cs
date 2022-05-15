using NUnit.Framework;
using SlayTheSpireSolver.Cards.Defend;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System.Linq;

namespace SlayTheSpireSolver.Tests.Cards.Defend;

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
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnemy()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenDefeated()
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(0) };
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        var gameState = CreateBasicGameState() with { Energy = new Energy(0) };
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void OneLegalActionWhenOneEnemy()
    {
        var gameState = CreateBasicGameState();
        var legalActions = new DefendCard().GetLegalActions(gameState).ToList();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new DefendAction(gameState), legalActions.First());
    }
}
