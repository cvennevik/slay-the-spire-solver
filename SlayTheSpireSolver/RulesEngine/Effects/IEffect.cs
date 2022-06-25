namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    ResolvableGameStatePossibilitySet Resolve(GameState gameState);
}