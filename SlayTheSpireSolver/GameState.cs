namespace SlayTheSpireSolver;

public record GameState
{
    public Player Player { get; init; }
    public IEnemy Enemy { get; init; }
    public Turn Turn { get; init; } = new Turn(1);
    public Hand Hand { get; init; } = new Hand();

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        var cardActions = Hand.Cards.ToList().SelectMany(card => card.GetLegalActions(this));
        return cardActions.Concat(new IAction[] { new EndTurnAction(this) }).ToArray();
    }
}
