namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory
{
    private readonly IEnemyMove[] _moves;

    public EnemyMoveHistory(params IEnemyMove[] moves)
    {
        _moves = moves;
    }

    public static bool operator ==(EnemyMoveHistory a, EnemyMoveHistory b) => a.Equals(b);
    public static bool operator !=(EnemyMoveHistory a, EnemyMoveHistory b) => !a.Equals(b);
    public override bool Equals(object? obj)
    {
        if (obj is not EnemyMoveHistory otherHistory) return false;
        return _moves.SequenceEqual(otherHistory._moves);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}