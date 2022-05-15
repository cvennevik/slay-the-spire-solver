using SlayTheSpireSolver.Enemies;

namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Contains(new StrikeCard());
    }

    public StrikeAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Strike action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var handWithStrikeRemoved = GameState.Hand.Remove(new StrikeCard());
        var enemy = GameState.EnemyParty.First();
        var enemyHealth = enemy.Health;
        var damagedEnemyHealth = new Health(enemyHealth.Value - 6);
        if (damagedEnemyHealth.Value < 1)
        {
            return GameState with { EnemyParty = new EnemyParty(), Hand = handWithStrikeRemoved };
        }
        var damagedEnemy = enemy with { Health = damagedEnemyHealth };
        return GameState with { EnemyParty = new EnemyParty(damagedEnemy), Hand = handWithStrikeRemoved };
    }
}
