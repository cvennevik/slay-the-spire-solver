using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameState
{
    public Health PlayerHealth { get; init; } = 1;
    public Armor PlayerArmor { get; init; } = 0;
    public Energy BaseEnergy { get; init; } = 3;
    public Energy Energy { get; init; } = 0;
    public EnemyParty EnemyParty { get; init; } = new();
    public Turn Turn { get; init; } = 1;
    public Hand Hand { get; init; } = new();
    public DrawPile DrawPile { get; init; } = new();
    public DiscardPile DiscardPile { get; init; } = new();

    public IReadOnlyCollection<Action> GetLegalActions()
    {
        var legalActions = new List<Action>();
        legalActions.AddRange(Hand.Cards.SelectMany(card => card.GetLegalActions(this)));
        if (!IsCombatOver())
        {
            legalActions.Add(new Action(this, new EndTurnEffect()));
        }
        return legalActions;
    }

    public bool IsCombatOver()
    {
        return PlayerHealth.Amount < 1 || !EnemyParty.Any();
    }

    public GameState ModifyEnemy(EnemyId id, Func<Enemy, Enemy> modifier)
    {
        return this with { EnemyParty = EnemyParty.ModifyEnemy(id, modifier) };
    }

    public ResolvableGameState WithEffects(params IEffect[] effects) => WithEffects(new EffectStack(effects));
    public ResolvableGameState WithEffects(EffectStack? effectStack = null)
    {
        return new ResolvableGameState(this, effectStack ?? new EffectStack());
    }

    public ResolvableGameStatePossibility WithProbability(Probability probability) => new(this, probability);

    public override string ToString()
    {
        return $@"GameState {{
    PlayerHealth: {PlayerHealth}
    PlayerArmor: {PlayerArmor}
    BaseEnergy: {BaseEnergy}
    Energy: {Energy}
    EnemyParty: {EnemyParty}
    Turn: {Turn}
    Hand: {Hand}
    DrawPile: {DrawPile}
    DiscardPile: {DiscardPile}
}}";
    }
}
