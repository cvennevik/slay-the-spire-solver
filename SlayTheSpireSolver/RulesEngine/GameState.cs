using SlayTheSpireSolver.RulesEngine.Cards;
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

    public GameState MoveCardFromHandToDiscardPile(ICard card)
    {
        return this with
        {
            Hand = Hand.Remove(card),
            DiscardPile = DiscardPile.Add(card)
        };
    }

    public GameState Remove(Energy energyToRemove)
    {
        return this with { Energy = Energy - energyToRemove };
    }

    public GameState DiscardHand()
    {
        return this with
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(DiscardPile.Cards.Concat(Hand.Cards).ToArray())
        };
    }

    public IReadOnlyList<GameState> DrawCard()
    {
        if (DrawPile.Cards.Count == 0)
        { 
            return DiscardPile.Cards.Any()
                ? ShuffleDiscardPileIntoDrawPile().DrawCard()
                : new[] { this };
        }

        var possibleStates = new List<GameState>();
        foreach (var card in DrawPile.Cards)
        {
            possibleStates.Add(this with
            {
                Hand = Hand.Add(card),
                DrawPile = DrawPile.Remove(card)
            });
        }
        return possibleStates.Distinct().ToArray();
    }

    public GameState ShuffleDiscardPileIntoDrawPile()
    {
        if (DiscardPile.Cards.Count == 0) return this;

        var newDrawPile = DrawPile;
        foreach (var card in DiscardPile.Cards)
        {
            newDrawPile = newDrawPile.Add(card);
        }

        return this with
        {
            DiscardPile = new DiscardPile(),
            DrawPile = newDrawPile
        };
    }

    public GameState ClearEnemyArmor()
    {
        var enemiesWithArmorCleared = EnemyParty.Select(enemy => enemy with { Armor = new Armor(0) });
        return this with { EnemyParty = new EnemyParty(enemiesWithArmorCleared.ToArray())};
    }
}
