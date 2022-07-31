using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine;

public class RelicCollection
{
    private readonly Relic[] _relics;

    public RelicCollection(params Relic[] relics)
    {
        Array.Sort(relics);
        _relics = relics;
    }

    public override string ToString()
    {
        return $"[{string.Join<Relic>(",", _relics)}]";
    }
}

[TestFixture]
internal class RelicCollectionTests
{
    [Test]
    public void TestEquality()
    {
    }
}