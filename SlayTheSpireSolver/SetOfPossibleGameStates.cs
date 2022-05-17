namespace SlayTheSpireSolver;

public record SetOfPossibleGameStates
{
    public IReadOnlyCollection<PossibleGameState> PossibleGameStates { get; }

    public SetOfPossibleGameStates(params PossibleGameState[] possibleGameStates)
    {
        PossibleGameStates = possibleGameStates;
    }
}
