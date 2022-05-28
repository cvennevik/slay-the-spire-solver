using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameState
{
    public Health PlayerHealth { get; init; } = new(1);
    public Armor PlayerArmor { get; init; } = new(0);
    public Energy Energy { get; init; } = new(0);
    public EnemyParty EnemyParty { get; init; } = new();
    public Turn Turn { get; init; } = new(1);
    public Hand Hand { get; init; } = new();
    public DrawPile DrawPile { get; init; } = new();
    public DiscardPile DiscardPile { get; init; } = new();

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
        return PlayerHealth.Amount < 1 || !EnemyParty.Any();
    }
}
