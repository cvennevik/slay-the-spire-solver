namespace SlayTheSpireSolver.RulesEngine.Effects;

public record NullEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}