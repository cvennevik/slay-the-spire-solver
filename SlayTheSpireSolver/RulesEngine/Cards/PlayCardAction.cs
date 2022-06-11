using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public readonly record struct PlayCardAction : IAction
{
    private readonly GameState _gameState;
    private readonly IEffect _effect;

    public static IReadOnlyCollection<IAction> GetLegalActions(GameState gameState, ICard card)
    {
        return IsLegal(gameState, card)
            ? new IAction[] { new PlayCardAction(gameState, card) }
            : Array.Empty<IAction>();
    }

    public static bool IsLegal(GameState gameState, ICard card)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(card)
               && gameState.Energy >= card.GetCost();
    }

    public PlayCardAction(GameState gameState, ICard card)
    {
        if (!IsLegal(gameState, card)) throw new ArgumentException("Illegal PlayCard action");
        _gameState = gameState;
        _effect = new CombinedEffect(
            new RemoveEnergyEffect(card.GetCost()),
            new RemoveCardFromHandEffect(card),
            card.GetEffect(gameState),
            new AddCardToDiscardPileEffect(card));
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return _effect.Resolve(_gameState).Select(x => x.GameState).ToList();
    }
}