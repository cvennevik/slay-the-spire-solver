namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction(GameState UnresolvedGameState)
{
    public readonly GameState UnresolvedGameState = UnresolvedGameState;

    public PossibilitySet Resolve() => UnresolvedGameState.Resolve();
}