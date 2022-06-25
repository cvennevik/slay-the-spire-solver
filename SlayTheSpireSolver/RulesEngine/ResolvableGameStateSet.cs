using System.Collections.ObjectModel;

namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStateSet : ReadOnlyCollection<ResolvableGameState>
{
    public ResolvableGameStateSet(IList<ResolvableGameState> list) : base(list) { }
}