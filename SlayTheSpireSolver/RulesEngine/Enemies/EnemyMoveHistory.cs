namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory
{
    private readonly IEnemyMove[] _moves;

    public EnemyMoveHistory(params IEnemyMove[] moves)
    {
        _moves = moves;
    }
}