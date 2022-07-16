﻿// See https://aka.ms/new-console-template for more information

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
Console.WriteLine("PROGRAM START");
Console.WriteLine("Evaluating Jaw Worm fight.");
Console.WriteLine($"GameState search depth: {solver.GameStateSearchDepth}");
Console.WriteLine("");
Console.WriteLine("Searching for expected value...");

var stopWatch = Stopwatch.StartNew();
var expectedValue = solver.FindExpectedValue(gameState);
stopWatch.Stop();

Console.WriteLine("Search complete.");
Console.WriteLine($"Expected value: {expectedValue}");
Console.WriteLine("");
Console.WriteLine("STATISTICS");
Console.WriteLine($"Elapsed time: {stopWatch.Elapsed}");
Console.WriteLine(
    $"Evaluated game states: {solver.EvaluatedGameStates}, cache hits: {solver.GameStateCacheHits}");
Console.WriteLine(
    $"Evaluated actions: {solver.EvaluatedActions}, cache hits: {solver.ActionCacheHits}");
