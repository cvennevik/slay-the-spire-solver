﻿using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver();
    }

    public EndTurnAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal EndTurn action");

        GameState = gameState;
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var actionWithEffectStack = new ActionWithEffectStack(GameState, new EffectStack(new EndTurnEffect()));
        return GameState
            .DiscardHand()
            .ClearEnemyArmor()
            .ResolveEnemyMoves()
            .IncrementTurn()
            .RecoverBaseEnergy()
            .DrawCard()
            .DrawCard()
            .DrawCard()
            .DrawCard()
            .DrawCard()
            .ToArray();
    }
}
