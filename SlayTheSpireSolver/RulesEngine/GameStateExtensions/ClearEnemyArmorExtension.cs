using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class ClearEnemyArmorExtension
{
    public static GameState ClearEnemyArmor(this GameState gameState)
    {
        var enemiesWithArmorCleared = gameState.EnemyParty.Select(enemy => enemy with { Armor = new Armor(0) });
        return gameState with { EnemyParty = new EnemyParty(enemiesWithArmorCleared.ToArray()) };
    }
}
