using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Thrash : IJawWormMove
{
    private static readonly Damage Damage = 7;
    private static readonly Armor ArmorGain = 5;

    public GameState Resolve(GameState gameState)
    {
        var newPlayerArmor = gameState.PlayerArmor - Damage;
        var newPlayerNealth = gameState.PlayerHealth - (Damage - gameState.PlayerArmor);

        var enemy = gameState.EnemyParty.FirstOrDefault();
        var newEnemyParty = gameState.EnemyParty;
        if (enemy != null)
        {
            enemy = enemy with { Armor = enemy.Armor + ArmorGain };
            var otherEnemies = gameState.EnemyParty.Skip(1).ToArray();
            newEnemyParty = new EnemyParty(new[] { enemy }.Concat(otherEnemies).ToArray());
        }

        return gameState with
        {
            PlayerHealth = newPlayerNealth,
            PlayerArmor = newPlayerArmor,
            EnemyParty = newEnemyParty
        };
    }

    public EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack(new GainEnemyArmorEffect(enemy.Id, ArmorGain), new ReducePlayerHealthEffect(Damage));
    }
}