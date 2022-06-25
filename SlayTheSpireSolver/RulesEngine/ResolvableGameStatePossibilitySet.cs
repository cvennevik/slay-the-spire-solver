using System.Collections;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStatePossibilitySet : IEnumerable<ResolvableGameStatePossibility>, IEquatable<ResolvableGameStatePossibilitySet>
{
    private readonly List<ResolvableGameState> _possibilities;

    public IEnumerator<ResolvableGameStatePossibility> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(ResolvableGameStatePossibilitySet? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _possibilities.Equals(other._possibilities);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ResolvableGameStatePossibilitySet)obj);
    }

    public override int GetHashCode()
    {
        return _possibilities.GetHashCode();
    }
}