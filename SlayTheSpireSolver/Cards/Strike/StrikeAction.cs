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
        var damagedEnemy = GameState.EnemyParty.First().Damage(6);
        if (damagedEnemy.Health.Value < 1)
        {
            return GameState with { EnemyParty = new EnemyParty(), Hand = handWithStrikeRemoved };
        }
        return GameState with { EnemyParty = new EnemyParty(damagedEnemy), Hand = handWithStrikeRemoved };
    }
}
