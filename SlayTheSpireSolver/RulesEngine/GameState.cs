﻿using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameState
{
    public Health PlayerHealth { get; init; } = 1;
    public Armor PlayerArmor { get; init; } = 0;
    public Energy BaseEnergy { get; init; } = 3;
    public Energy Energy { get; init; } = 0;
    public EnemyParty EnemyParty { get; init; } = new();
    public Turn Turn { get; init; } = 1;
    public Hand Hand { get; init; } = new();
    public DrawPile DrawPile { get; init; } = new();
    public DiscardPile DiscardPile { get; init; } = new();

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        var legalActions = new List<IAction>();
        legalActions.AddRange(Hand.Cards.SelectMany(card => card.GetLegalActions(this)));
        if (EndTurnAction.IsLegal(this))
        {
            legalActions.Add(new EndTurnAction(this));
        }
        return legalActions;
    }

    public bool IsCombatOver()
    {
        return PlayerHealth.Amount < 1 || !EnemyParty.Any();
    }

    public override string ToString()
    {
        return $@"GameState {{
    PlayerHealth: {PlayerHealth}
    PlayerArmor: {PlayerArmor}
    BaseEnergy: {BaseEnergy}
    Energy: {Energy}
    EnemyParty: {EnemyParty}
    Turn: {Turn}
    Hand: {Hand}
    DrawPile: {DrawPile}
    DiscardPile: {DiscardPile}
}}";
    }
}
