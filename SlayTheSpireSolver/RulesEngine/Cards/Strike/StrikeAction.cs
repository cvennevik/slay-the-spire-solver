using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Strike;

public readonly record struct StrikeAction : IAction
{
    private readonly GameState _gameState;
    private readonly IEffect _effect;

    private static readonly Energy EnergyCost = new(1);
    private static readonly Damage Damage = new(6);

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver()
            && gameState.Hand.Contains(new StrikeCard())
            && gameState.Energy >= EnergyCost;
    }

    public StrikeAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Strike action");
        _gameState = gameState;
        var targetEnemy = _gameState.EnemyParty.First();
        _effect = new CombinedEffect(
            new RemoveEnergyEffect(EnergyCost),
            new RemoveCardFromHandEffect(new StrikeCard()),
            new DamageEnemyEffect(targetEnemy, Damage),
            new AddCardToDiscardPileEffect(new StrikeCard()));
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return _effect.ApplyTo(_gameState);
    }
}
