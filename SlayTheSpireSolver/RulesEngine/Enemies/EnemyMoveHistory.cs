namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory
{
    public int Count => Moves.Length;
    public IEnemyMove[] Moves { get; }

    public EnemyMoveHistory(params IEnemyMove[] moves)
    {
        Moves = moves;
    }

    public static implicit operator EnemyMoveHistory(IEnemyMove[] moves) => new(moves);

    public static bool operator ==(EnemyMoveHistory a, EnemyMoveHistory b) => a.Equals(b);
    public static bool operator !=(EnemyMoveHistory a, EnemyMoveHistory b) => !a.Equals(b);
    public override bool Equals(object? obj)
    {
        return obj is EnemyMoveHistory otherHistory && Moves.SequenceEqual(otherHistory.Moves);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}