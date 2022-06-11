namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    IReadOnlyList<GameState> ApplyTo(GameState gameState);
    IReadOnlyCollection<GameStateWithUnresolvedEffects> Resolve(GameState gameState);
}