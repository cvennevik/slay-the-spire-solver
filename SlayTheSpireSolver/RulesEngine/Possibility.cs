using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
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
            .NewResolve(GameState)
            .Select(resolvablePossibility => resolvablePossibility with {Probability = resolvablePossibility.Probability * Probability})
            .ToArray();
    }
}

[TestFixture]
internal class PossibilityTests
{
    [Test]
    public void Test()
    {
        
    }
}