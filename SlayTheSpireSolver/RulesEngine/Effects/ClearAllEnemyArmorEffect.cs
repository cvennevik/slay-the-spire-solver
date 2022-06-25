namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearAllEnemyArmorEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newEnemyParty = gameState.EnemyParty;
        foreach (var enemy in gameState.EnemyParty)
        {
            newEnemyParty = newEnemyParty.ModifyEnemy(enemy.Id, enemy => enemy with { Armor = 0 });
        }

        return gameState with { EnemyParty = newEnemyParty };
    }
}