// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using SlayTheSpireSolver.AI;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

var jawWorm = new JawWorm
{
    Health = 44,
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

var solver = new Solver { GameStateSearchDepth = 4 };
Console.WriteLine("Evaluating Jaw Worm fight.");
Console.WriteLine($"GameState search depth: {solver.GameStateSearchDepth}");

var stopWatch = Stopwatch.StartNew();
var searchResult = solver.FindBestExpectedOutcome(gameState);
stopWatch.Stop();

Console.WriteLine(searchResult);
Console.WriteLine($"Elapsed time: {stopWatch.Elapsed}");
Console.WriteLine($"GameState cache size: {solver.GameStateCacheSize}, hits: {solver.GameStateCacheHits}");
Console.WriteLine($"PlayerAction cache size: {solver.ActionCacheSize}, hits: {solver.ActionCacheHits}");
