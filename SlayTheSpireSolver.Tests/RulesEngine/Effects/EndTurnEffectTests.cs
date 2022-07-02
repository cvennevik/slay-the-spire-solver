using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
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
            BaseEnergy = 3,
            Energy = 0,
            PlayerHealth = 50,
            PlayerArmor = 15,
            EnemyParty = new[]
            {
                new JawWorm { IntendedMove = new Chomp() },
                new JawWorm { IntendedMove = new Thrash() }
            },
            Hand = new Hand(new Strike(), new Defend()),
            DiscardPile = new DiscardPile(new Strike(), new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike())
        };
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new EndTurnEffect(), new EndTurnEffect());
    }
}