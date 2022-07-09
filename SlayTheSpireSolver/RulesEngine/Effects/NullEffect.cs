namespace SlayTheSpireSolver.RulesEngine.Effects;

public record NullEffect : Effect
{
    public override ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        return gameState;
    }
}