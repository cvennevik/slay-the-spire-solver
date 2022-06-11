using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public interface ICard
{
    Energy GetCost();
    IEffect GetEffect(GameState gameState);
}
