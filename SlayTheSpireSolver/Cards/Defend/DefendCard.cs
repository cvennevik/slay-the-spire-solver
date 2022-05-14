namespace SlayTheSpireSolver.Cards.Defend;

public record DefendCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        if (!gameState.EnemyParty.Any()) return Array.Empty<IAction>();
        if (!gameState.Hand.Cards.Contains(this)) return Array.Empty<IAction>();

        return new[] { new DefendAction(gameState) };
    }
}
