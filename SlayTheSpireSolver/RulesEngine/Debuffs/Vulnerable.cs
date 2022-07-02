namespace SlayTheSpireSolver.RulesEngine.Debuffs;

public record Vulnerable
{
    private readonly int Duration;

    public Vulnerable(int duration)
    {
        if (duration < 0) throw new ArgumentOutOfRangeException(nameof(duration));
        Duration = duration;
    }

    public static implicit operator Vulnerable(int duration) =>
        duration > 0 ? new Vulnerable(duration) : new Vulnerable(0);

    public Vulnerable Decrease()
    {
        return new Vulnerable(Duration - 1);
    }
}