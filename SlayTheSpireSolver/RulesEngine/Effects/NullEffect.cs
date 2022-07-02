namespace SlayTheSpireSolver.RulesEngine.Effects;

public record NullEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}