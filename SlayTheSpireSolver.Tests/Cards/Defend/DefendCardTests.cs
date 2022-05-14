using NUnit.Framework;
using SlayTheSpireSolver.Cards.Defend;
using SlayTheSpireSolver.Enemies.JawWorms;
using System.Linq;

namespace SlayTheSpireSolver.Tests.Cards.Defend;

[TestFixture]
public class DefendCardTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DefendCard(), new DefendCard());
    }

    [Test]
    public void NoLegalActionsWhenNoDefendCardInHand()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm()
        };
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnemy()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new DefendCard())
        };
        var legalActions = new DefendCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void OneLegalActionWhenOneEnemy()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new DefendCard()),
            Enemy = new JawWorm()
        };
        var legalActions = new DefendCard().GetLegalActions(gameState).ToList();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new DefendAction(gameState), legalActions.First());
    }
}
