using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : IEffect
{
    public IReadOnlyCollection<UnresolvedGameState> Apply(GameState gameState)
    {
        return new[] { gameState.RecoverBaseEnergy().WithEffectStack() };
    }
}