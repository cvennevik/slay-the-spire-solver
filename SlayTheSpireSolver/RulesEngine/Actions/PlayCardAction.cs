using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public interface PlayCardAction : PlayerAction
{
    public Card Card { get; }
}