using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;

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

    public SetOfPossibleGameStates DrawCard()
    {
        if (!DrawPile.Cards.Any()) return ToSetOfPossibleGameStates();

        var nextGameState = this with
        {
            DrawPile = new DrawPile(DrawPile.Cards.Skip(1).ToArray()),
            Hand = new Hand(Hand.Cards.Append(DrawPile.Cards[0]).ToArray())
        };
        return nextGameState.ToSetOfPossibleGameStates();
    }

    public GameState DiscardHand()
    {
        return this with
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(DiscardPile.Cards.Concat(Hand.Cards).ToArray())
        };
    }

    public SetOfPossibleGameStates ToSetOfPossibleGameStates()
    {
        return new SetOfPossibleGameStates(ToPossibleGameState());
    }

    public PossibleGameState ToPossibleGameState()
    {
        return new PossibleGameState(this, new Probability(1));
    }
}
