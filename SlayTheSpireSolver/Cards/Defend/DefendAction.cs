﻿namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    private const int EnergyCost = 1;
    private const int ArmorGainAmount = 5;

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Contains(new DefendCard()) && gameState.Energy.Amount >= EnergyCost;
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        return GameState
            .RemoveEnergy(EnergyCost)
            .MoveCardFromHandToDiscardPile(new DefendCard()) with
        {
            PlayerArmor = new Armor(GameState.PlayerArmor.Amount + ArmorGainAmount)
        };
    }
}
