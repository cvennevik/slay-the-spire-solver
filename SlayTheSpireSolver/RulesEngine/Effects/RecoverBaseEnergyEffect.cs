using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public IReadOnlyCollection<UnresolvedGameState> Resolve(GameState gameState)
    {
        return new[] { gameState.RecoverBaseEnergy().WithEffectStack() };
    }
}