using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    private static readonly Energy EnergyCost = new(1);
    private const int DamageAmount = 6;

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

    public GameState Resolve()
    {
        var damagedEnemy = GameState.EnemyParty.First().Damage(DamageAmount);
        var newEnemyParty = damagedEnemy.Health.Amount > 0
            ? new EnemyParty(damagedEnemy)
            : new EnemyParty();
        return GameState
            .Remove(EnergyCost)
            .MoveCardFromHandToDiscardPile(new StrikeCard()) with
        {
            EnemyParty = newEnemyParty
        };
    }
}
