using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record EndTurnAction : PlayerAction
{
    public EndTurnAction(GameState gameState) : base(gameState with { EffectStack = new[] { new EndTurnEffect() } })
    {
    }
}

[TestFixture]
internal class EndTurnEffectTests
{
    [Test]
    public void Test()
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
        var result = new EndTurnAction(gameState).Resolve();
        Assert.AreEqual(12, result.Count());
        Assert.AreEqual(1, result.Select(x => x.Probability.Value).Sum(), 0.0000000001);
        Assert.AreEqual(12, result.Count(x => x.GameState.EnemyParty.All(enemy => enemy.PreviousMoves.Count == 1)));
        Assert.AreEqual(12, result.Count(x => x.GameState.Turn == 2));
        Assert.AreEqual(12, result.Count(x => x.GameState.PlayerHealth.Current == 47));
    }

    [Test]
    public void TestWithLotsOfArmor()
    {
        var gameState = new GameState
        {
            BaseEnergy = 3,
            Energy = 0,
            PlayerHealth = 50,
            PlayerArmor = 50,
            EnemyParty = new[]
            {
                new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() },
                new JawWorm { Id = EnemyId.New(), IntendedMove = new Thrash() }
            },
            Hand = new Hand(new Strike(), new Defend()),
            DiscardPile = new DiscardPile(new Strike(), new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike())
        };
        var result = new EndTurnAction(gameState).Resolve();
        Assert.AreEqual(12, result.Count());
        Assert.AreEqual(1, result.Select(x => x.Probability.Value).Sum(), 0.0000000001);
        Assert.AreEqual(12, result.Count(x => x.GameState.EnemyParty.All(enemy => enemy.PreviousMoves.Count == 1)));
        Assert.AreEqual(12, result.Count(x => x.GameState.Turn == 2));
        Assert.AreEqual(12, result.Count(x => x.GameState.PlayerHealth == new Health(50)));
        Assert.AreEqual(12, result.Count(x => x.GameState.PlayerArmor == 0));
    }

    [Test]
    public void ExhaustsAscendersBane()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new AscendersBane(), new Defend())
        };
        var result = new EndTurnAction(gameState).Resolve();
        Assert.True(result
            .Select(x => x.GameState)
            .All(x => x.ExhaustPile == new ExhaustPile(new AscendersBane()))
        );
    }
}