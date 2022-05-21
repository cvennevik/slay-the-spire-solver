namespace SlayTheSpireSolver.RulesEngine;

public record Turn
{
    public int Number { get; }

    public Turn(int number)
    {
        if (number < 1) throw new ArgumentOutOfRangeException(nameof(number));

        Number = number;
    }
}
