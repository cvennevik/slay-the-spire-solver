using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards.Strike;

public readonly record struct StrikeAction : IAction
{
    private readonly GameState _gameState;

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
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var damagedEnemy = _gameState.EnemyParty.First().DealDamage(Damage);
        var newEnemyParty = damagedEnemy.Health.Amount > 0
            ? new EnemyParty(damagedEnemy)
            : new EnemyParty();
        var resolvedState = _gameState
                .Remove(EnergyCost)
                .MoveCardFromHandToDiscardPile(new StrikeCard()) with
            {
                EnemyParty = newEnemyParty
            };
        return new[] { resolvedState };
    }
}
