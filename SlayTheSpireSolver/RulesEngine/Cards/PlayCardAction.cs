using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public readonly record struct PlayCardAction : IAction
{
    private readonly GameState _gameState;
    private readonly IEffect _effect;

    public PlayCardAction(GameState gameState, Card card)
    {
        if (!card.CanBePlayed(gameState)) throw new ArgumentException("Illegal PlayCard action");
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