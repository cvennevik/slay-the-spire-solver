namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState);
}