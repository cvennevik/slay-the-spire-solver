// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using SlayTheSpireSolver.AI;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

var jawWorm = new JawWorm
{
    Health = new Health(44, 44),
    IntendedMove = new Chomp()
};
var gameState = new GameState
{
    PlayerHealth = new Health(80, 80),
    BaseEnergy = 3,
    Energy = 3,
    EnemyParty = new[] { jawWorm },
    Hand = new Hand(new Strike(), new Strike(), new Strike(), new Bash(), new Defend()),
    DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike(), new Strike(), new AscendersBane()),
    Turn = 1
};

Console.WriteLine("PROGRAM START");
Console.WriteLine("Evaluating Jaw Worm fight.");
Console.WriteLine($"Initial game state: {gameState}");
Console.WriteLine("");

for (var i = 1; i <= 14; i++)
{
    var solver = new Solver(i);
    Console.WriteLine($"Searching for best player action, search depth: {solver.GameStateSearchDepth}...");

    var stopWatch = Stopwatch.StartNew();
    var (bestAction, expectedValue) = solver.FindBestAction(gameState);
    stopWatch.Stop();

    Console.WriteLine($"Recommended action: {bestAction}");
    Console.WriteLine($"Expected value: {expectedValue}");
    Console.WriteLine("");
    Console.WriteLine($"Elapsed time: {stopWatch.Elapsed}");
    Console.WriteLine($"Evaluated game states: {solver.EvaluatedGameStates}, cache hits: {solver.GameStateCacheHits}");
    Console.WriteLine($"Pruned action outcomes: {solver.PrunedActionOutcomes}");
    Console.WriteLine("---");
}