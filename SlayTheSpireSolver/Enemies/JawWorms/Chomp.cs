namespace SlayTheSpireSolver.Enemies.JawWorms;

public record Chomp : IJawWormMove
{
    private const int Damage = 12;

    public GameState Resolve(GameState gameState)
    {
        var player = gameState.Player;
        var playerHealth = player.Health;
        var newPlayerHealth = new Health(playerHealth.Value - Damage);
        var newGameState = gameState with { Player = player with { Health = newPlayerHealth } };
        return newGameState;
    }
}
