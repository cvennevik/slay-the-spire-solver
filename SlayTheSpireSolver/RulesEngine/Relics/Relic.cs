namespace SlayTheSpireSolver.RulesEngine.Relics;

public interface Relic : IComparable<Relic>
{
    string Name { get; }
}