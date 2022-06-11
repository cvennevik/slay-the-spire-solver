using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Strike;

public record StrikeCard : ICard
{
    private static readonly Energy Cost = new(1);

    public Energy GetCost() => Cost;
    public IEffect GetEffect(GameState gameState) => new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(6));

    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return PlayCardAction.IsLegal(gameState, this)
            ? new IAction[] { new PlayCardAction(gameState, this) }
            : Array.Empty<IAction>();
    }
}
