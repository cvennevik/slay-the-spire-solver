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

const int gameStateSearchDepth = 4;
Console.WriteLine("Evaluating Jaw Worm fight.");
Console.WriteLine($"GameState search depth: {gameStateSearchDepth}");

var stopWatch = Stopwatch.StartNew();
var searchResult = new Solver().FindBestExpectedOutcome(gameState, gameStateSearchDepth);
stopWatch.Stop();

Console.WriteLine(searchResult);
Console.WriteLine($"Elapsed time: {stopWatch.Elapsed}");
