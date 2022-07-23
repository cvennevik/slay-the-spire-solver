using BenchmarkDotNet.Attributes;
using SlayTheSpireSolver.AI;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Benchmark;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    [Benchmark]
    public (PlayerAction, ExpectedValue) MyFirstBenchmark()
    {
        var jawWorm = new JawWorm
        {
            Health = 30,
            IntendedMove = new Chomp()
        };
        var gameState = new GameState
        {
            PlayerHealth = 80,
            BaseEnergy = 3,
            Energy = 3,
            EnemyParty = new[] { jawWorm },
            Hand = new Hand(new Strike(), new Strike(), new Strike(), new Bash(), new Defend()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike(), new Strike()),
            Turn = 1
        };
        return new Solver(3).FindBestAction(gameState);
    }
}