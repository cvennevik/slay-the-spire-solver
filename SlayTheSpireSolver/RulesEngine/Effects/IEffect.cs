namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyList<GameState> ApplyTo(GameState gameState);
    IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState);
}