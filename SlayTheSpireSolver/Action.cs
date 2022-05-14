namespace SlayTheSpireSolver;

public abstract record Action
{
    public abstract GameState Do();
}
