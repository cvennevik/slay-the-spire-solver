namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearAllEnemyArmorEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        var enemies = gameState.EnemyParty.ToList();
        var newEnemyParty = gameState.EnemyParty;
        foreach (var enemy in enemies)
        {
            newEnemyParty = newEnemyParty.ModifyEnemy(enemy.Id, enemy => enemy with { Armor = 0 });
        }

        return gameState with { EnemyParty = newEnemyParty };
    }
}