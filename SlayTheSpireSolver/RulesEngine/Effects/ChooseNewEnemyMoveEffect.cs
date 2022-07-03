using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ChooseNewEnemyMoveEffect : TargetEnemyEffect
{
    public ChooseNewEnemyMoveEffect() { }
    public ChooseNewEnemyMoveEffect(EnemyId target) : base(target) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;

        gameState = gameState.ModifyEnemy(Target, enemy =>
            enemy with { PreviousMoves = enemy.PreviousMoves.Concat(new[] { enemy.IntendedMove }).ToArray() });
        var possibleMoves = gameState.EnemyParty.Get(Target).GetNextPossibleMoves();
        var result = possibleMoves.Select(moveAndProbability =>
            gameState.ModifyEnemy(Target, enemy =>
                    enemy with { IntendedMove = moveAndProbability.Item1 })
                .WithProbability(moveAndProbability.Item2)
        ).ToArray();

        return result;
    }
}

[TestFixture]
internal class ChooseNewEnemyMoveEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
        var effect = new ChooseNewEnemyMoveEffect(EnemyId.New());
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsIntendedMoveToPreviousMovesAndSetsNewIntendedMove()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
        var effect = new ChooseNewEnemyMoveEffect(EnemyId.Default);
        var result = effect.Resolve(gameState);
        var bellowEnemy = new JawWorm { IntendedMove = new Bellow(), PreviousMoves = new[] { new Chomp() } };
        var thrashEnemy = new JawWorm { IntendedMove = new Thrash(), PreviousMoves = new[] { new Chomp() } };
        var expectedPossibilities = new ResolvablePossibility[]
        {
            (gameState with { EnemyParty = new[] { bellowEnemy } }).WithProbability(0.45 / 0.75),
            (gameState with { EnemyParty = new[] { thrashEnemy } }).WithProbability(0.3 / 0.75)
        };
        Assert.That(result, Is.EquivalentTo(expectedPossibilities));
    }
}