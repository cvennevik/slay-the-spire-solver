using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record Possibility(GameState GameState, Probability Probability)
{
    public static implicit operator Possibility(GameState gameState) => new(gameState, new Probability(1));

    public bool IsEqualTo(Possibility other, double tolerance = double.Epsilon)
    {
        return GameState == other.GameState && Probability.IsEqualTo(other.Probability, tolerance);
    }

    public IReadOnlyList<Possibility> Resolve()
    {
        if (GameState.EffectStack.IsEmpty())
        {
            return new[] { this };
        }

        return ResolveTopEffect()
            .SelectMany(x => x.Resolve())
            .GroupBy(x => x.GameState)
            .Select(grouping => new Possibility(grouping.Key,
                grouping.Select(x => x.Probability).Aggregate((acc, x) => acc.Add(x))))
            .ToList();
    }

    private IReadOnlyList<Possibility> ResolveTopEffect()
    {
        var (effect, remainingEffectStack) = GameState.EffectStack.Pop();
        return effect
            .NewResolve(GameState with {EffectStack = remainingEffectStack})
            .Select(possibility => possibility with {Probability = possibility.Probability * Probability})
            .ToArray();
    }
}

[TestFixture]
internal class PossibilityTests
{
    [Test]
    public void TestEmptyEffectStack()
    {
        var gameState = new GameState { Turn = 3 };
        var possibility = new Possibility(gameState, 0.7);
        var result = possibility.Resolve().Single();
        Assert.AreEqual(possibility, result);
    }

    [Test]
    public void TestEffectsProducingNewEffects()
    {
        var gameState = new GameState
        {
            Turn = 2,
            EnemyParty = new EnemyParty(new JawWorm { Health = 2 }),
            EffectStack = new AttackEnemyEffect(EnemyId.Default, 3)
        };
        var possibility = new Possibility(gameState, 0.8);
        var result = possibility.Resolve();
        var expectedGameState = new GameState
        {
            Turn = 2,
            EnemyParty = new EnemyParty(),
            EffectStack = new EffectStack()
        };
        var expectedPossibility = new Possibility(expectedGameState, 0.8);
    }

    [Test]
    public void TestMultipleResults()
    {
        var gameState = new GameState
        {
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Strike(), new Defend()),
            EffectStack = new EffectStack(new DrawCardEffect(), new DrawCardEffect())
        };
        var possibility = new Possibility(gameState, new Probability(0.5));
        var result = possibility.Resolve();
        var expectedGameState1 = new GameState
        {
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike()),
            Hand = new Hand(new Strike(), new Defend())
        };
        var expectedGameState2 = new GameState
        {
            DrawPile = new DrawPile(new Strike(), new Strike(), new Defend()),
            Hand = new Hand(new Strike(), new Strike())
        };
        var expectedPossibility1 = new Possibility(expectedGameState1, new Probability(0.2));
        var expectedPossibility2 = new Possibility(expectedGameState2, new Probability(0.3));
        const double tolerance = 0.0000000000000001;
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(0.5, result.Sum(x => x.Probability.Value));
        Assert.AreEqual(1, result.Count(x => x.GameState == expectedGameState1));
        Assert.AreEqual(1, result.Count(x => x.GameState == expectedGameState2));
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedPossibility1, tolerance)));
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedPossibility2, tolerance)));
    }
}