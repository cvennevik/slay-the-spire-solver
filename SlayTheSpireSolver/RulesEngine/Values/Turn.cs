namespace SlayTheSpireSolver.RulesEngine.Values;

public record Turn
{
    public int Number { get; }

    public Turn(int number)
    {
        if (number < 1) throw new ArgumentOutOfRangeException(nameof(number));

        Number = number;
    }

    public static implicit operator Turn(int amount) => amount > 1 ? new Turn(amount) : new Turn(1);
}
