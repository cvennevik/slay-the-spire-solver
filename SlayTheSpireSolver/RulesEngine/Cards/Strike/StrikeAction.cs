using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    private static readonly Energy EnergyCost = new(1);
    private static readonly Damage Damage = new(6);

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver()
            && gameState.Hand.Contains(new StrikeCard())
            && gameState.Energy >= EnergyCost;
    }

    public StrikeAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Strike action");
        GameState = gameState;
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var damagedEnemy = GameState.EnemyParty.First().DealDamage(Damage);
        var newEnemyParty = damagedEnemy.Health.Amount > 0
            ? new EnemyParty(damagedEnemy)
            : new EnemyParty();
        var resolvedState = GameState
                .Remove(EnergyCost)
                .MoveCardFromHandToDiscardPile(new StrikeCard()) with
            {
                EnemyParty = newEnemyParty
            };
        return new[] { resolvedState };
    }
}
