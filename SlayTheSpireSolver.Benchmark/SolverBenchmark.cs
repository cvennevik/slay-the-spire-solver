using BenchmarkDotNet.Attributes;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Benchmark;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    private readonly GameState _gameState = new()
    {
        PlayerHealth = 80,
        BaseEnergy = 3,
        Energy = 3,
        EnemyParty = new[]
        {
            new JawWorm
            {
                Health = 30,
                IntendedMove = new Chomp()
            }
        },
        Hand = new Hand(new Strike(), new Strike(), new Strike(), new Bash(), new Defend()),
        DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike(), new Strike()),
        Turn = 1
    };

    [Benchmark]
    public (PlayerAction, ExpectedValue) SolveDepth3()
    {
        return new Solver(3).FindBestAction(_gameState);
    }
}