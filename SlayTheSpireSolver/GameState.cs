using SlayTheSpireSolver.Enemies;

namespace SlayTheSpireSolver;

public record GameState
{
    public Player Player { get; init; }
    public Enemy Enemy { get; init; }
    public Turn Turn { get; init; } = new Turn(1);
    public Hand Hand { get; init; } = new Hand();

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        var legalActions = new List<IAction>();
        legalActions.AddRange(Hand.Cards.ToList().SelectMany(card => card.GetLegalActions(this)));
        if (Enemy != null)
        {
            legalActions.Add(new EndTurnAction(this));
        }
        return legalActions;
    }

    public bool IsDefeat()
    {
        return Player.Health.Value < 1;
    }

    public bool IsVictory()
    {
        return Enemy == null && Player.Health.Value > 0;
    }
}
