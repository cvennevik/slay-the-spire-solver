namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearAllEnemyArmorEffect : IEffect
{
    public IReadOnlyCollection<UnresolvedGameState> Apply(GameState gameState)
    {
        var enemies = gameState.EnemyParty.ToList();
        var newEnemyParty = gameState.EnemyParty;
        foreach (var enemy in enemies)
        {
            newEnemyParty = newEnemyParty.ModifyEnemy(enemy.Id, enemy => enemy with { Armor = 0 });
        }

        var result = gameState with { EnemyParty = newEnemyParty };
        return new[] { result.WithEffectStack() };
    }
}