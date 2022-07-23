namespace SlayTheSpireSolver.RulesEngine.Effects;

public record NullEffect : Effect
{
    public virtual PossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}