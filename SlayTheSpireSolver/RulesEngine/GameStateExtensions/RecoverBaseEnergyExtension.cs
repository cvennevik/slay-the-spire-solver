namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class RecoverBaseEnergyExtension
{
    public static GameState RecoverBaseEnergy(this GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}
