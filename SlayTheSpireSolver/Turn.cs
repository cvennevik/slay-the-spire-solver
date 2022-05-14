namespace SlayTheSpireSolver;

public record Turn
{
    public int Number { get; init; }

    public Turn(int number)
    {
        if (number < 1) throw new ArgumentOutOfRangeException(nameof(number));

        Number = number;
    }
}
