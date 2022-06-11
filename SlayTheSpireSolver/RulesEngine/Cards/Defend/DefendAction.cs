using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    private static readonly Energy EnergyCost = new(1);

    private static readonly IEffect Effect = new CombinedEffect(
        new RemoveEnergyEffect(1),
        new RemoveCardFromHandEffect(new DefendCard()),
        new GainPlayerArmorEffect(5),
        new AddCardToDiscardPileEffect(new DefendCard()));

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver()
            && gameState.Hand.Contains(new DefendCard())
            && gameState.Energy >= EnergyCost;
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return Effect.ApplyTo(GameState);
    }
}
