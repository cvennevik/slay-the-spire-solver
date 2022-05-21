namespace SlayTheSpireSolver.RulesEngine;

public class SetOfPossibleGameStates
{
    public IReadOnlyCollection<PossibleGameState> PossibleGameStates { get; }

    public SetOfPossibleGameStates(params PossibleGameState[] possibleGameStates)
    {
        var sumOfProbabilities = possibleGameStates.Select(x => x.Probability.Value).Sum();
        if (sumOfProbabilities > 1) throw new ArgumentException("Sum of probabilities cannot exceed 1");

        PossibleGameStates = possibleGameStates
            .GroupBy(x => x.GameState)
            .Select(grouping => new PossibleGameState(
                grouping.Key,
                new Probability(grouping.Select(possibleGameState => possibleGameState.Probability.Value).Sum())
            ))
            .ToArray();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not SetOfPossibleGameStates otherSet) return false;
        if (otherSet.PossibleGameStates.Count != PossibleGameStates.Count) return false;
        foreach (var possibleGameState in PossibleGameStates)
        {
            if (!otherSet.PossibleGameStates.Contains(possibleGameState)) return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
