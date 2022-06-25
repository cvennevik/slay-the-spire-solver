namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyCollection<UnresolvedGameState> Apply(GameState gameState);
}