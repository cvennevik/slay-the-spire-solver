using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndTurnEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState.WithEffects(new EffectStack(
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
            new MoveHandToDiscardPileEffect()));
    }
}

[TestFixture]
internal class EndTurnEffectTests
{
    [Test]
    public void TestResolvingFully()
    {
        var gameState = new GameState
        {
            BaseEnergy = 3,
            Energy = 0,
            PlayerHealth = 50,
            PlayerArmor = 15,
            EnemyParty = new[]
            {
                new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() },
                new JawWorm { Id = EnemyId.New(), IntendedMove = new Thrash() }
            },
            Hand = new Hand(new Strike(), new Defend()),
            DiscardPile = new DiscardPile(new Strike(), new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike())
        };
        var result = gameState.WithEffects(new EndTurnEffect()).Resolve();
        Assert.AreEqual(12, result.Count);
        Assert.AreEqual(1, result.Select(x => x.Probability.Value).Sum(), double.Epsilon);
        Assert.AreEqual(12, result.Count(x => x.GameState.EnemyParty.All(enemy => enemy.PreviousMoves.Count == 1)));
        Assert.AreEqual(12, result.Count(x => x.GameState.Turn == 2));
        Assert.AreEqual(12, result.Count(x => x.GameState.PlayerHealth == 46));
    }
}