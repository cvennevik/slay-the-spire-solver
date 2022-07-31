namespace SlayTheSpireSolver.RulesEngine.Relics;

public abstract record Relic : IComparable<Relic>
{
    public abstract string Name { get; }

    public int CompareTo(Relic? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }
}