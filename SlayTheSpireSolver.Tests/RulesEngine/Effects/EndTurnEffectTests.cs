using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class EndTurnEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { Turn = 2 };
        var effect = new EndTurnEffect();
        var newGameStateWithEffectStack = effect.Resolve(gameState).SingleUnresolvedState();
        var expectedEffectStack = new EffectStack(
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new RecoverBaseEnergyEffect(),
            new IncrementTurnEffect(),
            new ResolveForAllEnemiesEffect<ChooseNewEnemyMoveEffect>(),
            new ResolveForAllEnemiesEffect<ResolveEnemyMoveEffect>(),
            new ClearAllEnemyArmorEffect(),
            new MoveHandToDiscardPileEffect());
        var expectedResult = new ResolvableGameState(gameState, expectedEffectStack);
        Assert.AreEqual(expectedResult, newGameStateWithEffectStack);
    }

    [Test]
    public void TestFullResolution()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            PlayerArmor = 15,
            EnemyParty = new[]
            {
                new JawWorm { IntendedMove = new Chomp() },
                new JawWorm { IntendedMove = new Thrash() }
            }
        };
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new EndTurnEffect(), new EndTurnEffect());
    }
}