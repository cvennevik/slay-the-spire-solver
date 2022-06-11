using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class RemoveEnergyExtension
{
    public static GameState Remove(this GameState gameState, Energy energyToRemove)
    {
        return gameState with { Energy = gameState.Energy - energyToRemove };
    }
}
