namespace SlayTheSpireSolver.Enemies.JawWorms;

public record JawWorm : IEnemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    public IEnemyMove GetIntendedMove() => IntendedMove;
}
