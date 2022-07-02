namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory : List<IEnemyMove>
{
    public EnemyMoveHistory(params IEnemyMove[] moves) : base(moves)
    {
    }

    public static implicit operator EnemyMoveHistory(IEnemyMove[] moves) => new(moves);

    public static bool operator ==(EnemyMoveHistory a, EnemyMoveHistory b) => a.Equals(b);
    public static bool operator !=(EnemyMoveHistory a, EnemyMoveHistory b) => !a.Equals(b);
    public override bool Equals(object? obj)
    {
        return obj is EnemyMoveHistory otherHistory && this.SequenceEqual(otherHistory);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}