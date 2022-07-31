using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine;

public class RelicCollection
{
    private readonly Relic[] _relics;

    public RelicCollection(params Relic[] relics)
    {
        _relics = relics;
    }
}