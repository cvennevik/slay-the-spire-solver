namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState);
}