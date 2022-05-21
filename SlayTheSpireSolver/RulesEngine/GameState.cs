using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameState
{
    public Health PlayerHealth { get; init; } = new Health(1);
    public Armor PlayerArmor { get; init; } = new Armor(0);
    public Energy Energy { get; init; } = new Energy(0);
    public EnemyParty EnemyParty { get; init; } = new EnemyParty();
    public Turn Turn { get; init; } = new Turn(1);
    public Hand Hand { get; init; } = new Hand();
    public DrawPile DrawPile { get; init; } = new DrawPile();
    public DiscardPile DiscardPile { get; init; } = new DiscardPile();

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
            return new[] { this };
        }

        var possibleStates = new List<GameState>();
        foreach (var card in DrawPile.Cards)
        {
            var drawPileCards = DrawPile.Cards.ToList();
            drawPileCards.Remove(card);
            possibleStates.Add(this with
            {
                Hand = Hand.Add(card),
                DrawPile = new DrawPile(drawPileCards.ToArray())
            });
        }
        return possibleStates.Distinct().ToArray();
    }
}
