namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    ResolvablePossibilitySet Resolve(GameState gameState);
}