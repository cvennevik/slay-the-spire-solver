namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory
{
    public int Count => _moves.Length;
    private readonly IEnemyMove[] _moves;

    public EnemyMoveHistory(params IEnemyMove[] moves)
    {
        _moves = moves;
    }

    public static implicit operator EnemyMoveHistory(IEnemyMove[] moves) => new(moves);

    public static bool operator ==(EnemyMoveHistory a, EnemyMoveHistory b) => a.Equals(b);
    public static bool operator !=(EnemyMoveHistory a, EnemyMoveHistory b) => !a.Equals(b);
    public override bool Equals(object? obj)
    {
        return obj is EnemyMoveHistory otherHistory && _moves.SequenceEqual(otherHistory._moves);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}