using NUnit.Framework;
using SlayTheSpireSolver.AI;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.AI;

[TestFixture]
public class SolverTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState
        {
            PlayerHealth = 70,
            EnemyParty = new EnemyParty(new JawWorm()),
        };
        var bestAction = Solver.GetBestAction(gameState);
        Assert.AreEqual(new EndTurnAction(gameState), bestAction);
    }

    [Test]
    public void Test2()
    {
        var gameState = new GameState
        {
            PlayerHealth = 70,
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = 6 }),
            Hand = new Hand(new Strike())
        };
        var bestAction = Solver.GetBestAction(gameState);
        var strike = new Strike();
        var playStrikeAction = new ActionWithEffectStack(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(strike),
            strike.GetEffect(gameState),
            new RemoveCardFromHandEffect(strike),
            new RemoveEnergyEffect(strike.GetCost())));
        Assert.AreEqual(playStrikeAction, bestAction);
    }
}
