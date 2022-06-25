using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return new[] { gameState.RecoverBaseEnergy().AsResolvable() };
    }
}