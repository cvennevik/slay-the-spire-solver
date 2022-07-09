namespace SlayTheSpireSolver.RulesEngine.Effects;

public record NullEffect : Effect
{
    public override PossibilitySet NewResolve(GameState gameState)
    {
        return gameState;
    }
}