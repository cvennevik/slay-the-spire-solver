namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    PossibilitySet Resolve(GameState gameState);
}