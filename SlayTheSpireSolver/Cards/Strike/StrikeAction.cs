using SlayTheSpireSolver.Enemies;

namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Contains(new StrikeCard()) && gameState.Energy.Amount >= 1;
    }

    public StrikeAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Strike action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var handWithStrikeRemoved = GameState.Hand.Remove(new StrikeCard());
        var discardPileCards = GameState.DiscardPile.Cards.ToList();
        discardPileCards.Add(new StrikeCard());
        var discardPileWithStrikeAdded = new DiscardPile(discardPileCards.ToArray());
        var damagedEnemy = GameState.EnemyParty.First().Damage(6);
        var newEnemyParty = damagedEnemy.Health.Amount > 0
            ? new EnemyParty(damagedEnemy)
            : new EnemyParty();
        var reducedEnergy = new Energy(GameState.Energy.Amount - 1);
        return GameState with
        {
            Energy = reducedEnergy,
            EnemyParty = newEnemyParty,
            Hand = handWithStrikeRemoved,
            DiscardPile = discardPileWithStrikeAdded
        };
    }
}
