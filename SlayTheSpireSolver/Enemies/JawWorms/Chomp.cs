namespace SlayTheSpireSolver.Enemies.JawWorms;

public record Chomp : IJawWormMove
{
    private const int Damage = 12;

    public GameState Resolve(GameState gameState)
    {
        if (Damage > gameState.PlayerArmor.Amount)
        {
            var remainingDamage = Damage - gameState.PlayerArmor.Amount;
            var newPlayerHealth = new Health(gameState.PlayerHealth.Amount - remainingDamage);
            return gameState with { PlayerHealth = newPlayerHealth, PlayerArmor = new Armor(0) };
        }
        else
        {
            var newPlayerArmor = new Armor(gameState.PlayerArmor.Amount - Damage);
            return gameState with { PlayerArmor = newPlayerArmor };
        }
    }
}
