namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyCollection<UnresolvedGameState> Resolve(GameState gameState);
}