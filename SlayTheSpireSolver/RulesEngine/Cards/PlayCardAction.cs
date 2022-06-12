using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public readonly record struct PlayCardAction : IAction
{
    private readonly GameState _gameState;
    private readonly IEffect _effect;

    public static bool IsLegal(GameState gameState, Card card)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(card)
               && gameState.Energy >= card.GetCost();
    }

    public PlayCardAction(GameState gameState, Card card)
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