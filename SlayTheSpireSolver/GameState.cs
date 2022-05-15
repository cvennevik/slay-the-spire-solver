using SlayTheSpireSolver.Enemies;

namespace SlayTheSpireSolver;

public record GameState
{
    public Player Player { get; init; } = new Player();
    public EnemyParty EnemyParty { get; init; } = new EnemyParty();
    public Turn Turn { get; init; } = new Turn(1);
    public Hand Hand { get; init; } = new Hand();

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        var legalActions = new List<IAction>();
        legalActions.AddRange(Hand.Cards.ToList().SelectMany(card => card.GetLegalActions(this)));
        if (EndTurnAction.IsLegal(this))
        {
            legalActions.Add(new EndTurnAction(this));
        }
        return legalActions;
    }

    public bool IsCombatOver()
    {
        return Player.Health.Value < 1 || !EnemyParty.Any();
    }
}
