using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState.RecoverBaseEnergy();
    }
}