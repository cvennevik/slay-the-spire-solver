namespace SlayTheSpireSolver.RulesEngine.Debuffs;

public record Vulnerable
{
    public int Duration { get; }

    public Vulnerable(int duration)
    {
        if (duration < 0) throw new ArgumentOutOfRangeException(nameof(duration));
        Duration = duration;
    }
}