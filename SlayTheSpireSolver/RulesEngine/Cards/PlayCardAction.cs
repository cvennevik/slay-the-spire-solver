using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public readonly record struct PlayCardAction : IAction
{
    private readonly GameState _gameState;
    private readonly EffectStack _effectStack;

    public PlayCardAction(GameState gameState, Card card)
    {
        if (!card.CanBePlayed(gameState)) throw new ArgumentException("Illegal PlayCard action");
        _gameState = gameState;
        _effectStack = new EffectStack(
            new AddCardToDiscardPileEffect(card),
            card.GetEffect(gameState),
            new RemoveCardFromHandEffect(card),
            new RemoveEnergyEffect(card.GetCost()));
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var remainingEffectStack = _effectStack;
        var workingGameState = _gameState;
        while (remainingEffectStack != new EffectStack())
        {
            (var effect, remainingEffectStack) = remainingEffectStack.Pop();
            workingGameState = effect.Resolve(workingGameState).Select(x => x.GameState).Single();
        }

        return new[] { workingGameState };
    }
}