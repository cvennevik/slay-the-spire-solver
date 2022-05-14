namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    public StrikeAction(GameState gameState)
    {
        if (gameState.Enemy == null) throw new ArgumentException("No enemy to attack");
        if (!gameState.Hand.Cards.Contains(new StrikeCard())) throw new ArgumentException("No Strike card in hand");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var handCardsCopy = GameState.Hand.Cards.ToList();
        handCardsCopy.Remove(new StrikeCard());
        var handWithStrikeRemoved = new Hand(handCardsCopy.ToArray());
        var enemy = GameState.Enemy;
        var enemyHealth = enemy.Health;
        var damagedEnemyHealth = new Health(enemyHealth.Value - 6);
        var damagedEnemy = enemy with { Health = damagedEnemyHealth };
        return GameState with { Enemy = damagedEnemy, Hand = handWithStrikeRemoved };
    }
}
