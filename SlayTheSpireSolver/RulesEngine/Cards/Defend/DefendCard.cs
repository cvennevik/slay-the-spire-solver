﻿namespace SlayTheSpireSolver.RulesEngine.Cards.Defend;

public record DefendCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return DefendAction.IsLegal(gameState)
            ? new IAction[] { new DefendAction(gameState) }
            : Array.Empty<IAction>();
    }
}
