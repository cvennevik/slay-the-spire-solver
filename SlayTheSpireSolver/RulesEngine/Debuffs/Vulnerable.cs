namespace SlayTheSpireSolver.RulesEngine.Debuffs;

public record Vulnerable
{
    private readonly int _duration;

    public Vulnerable(int duration)
    {
        if (duration < 0) throw new ArgumentOutOfRangeException(nameof(duration));
        _duration = duration;
    }

    public static implicit operator Vulnerable(int duration) =>
        duration > 0 ? new Vulnerable(duration) : new Vulnerable(0);

    public Vulnerable Decrease()
    {
        return new Vulnerable(_duration - 1);
    }
}