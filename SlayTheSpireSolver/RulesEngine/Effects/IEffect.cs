namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface IEffect
{
    ResolvableGameStateSet Resolve(GameState gameState);
}