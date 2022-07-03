namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayCardAction : PlayerAction
{
    protected PlayCardAction(ResolvableGameState resolvableGameState) : base(resolvableGameState) { }
}