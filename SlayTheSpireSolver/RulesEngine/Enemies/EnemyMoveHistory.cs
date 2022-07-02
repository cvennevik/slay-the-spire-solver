namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory : List<IEnemyMove>
{
    private readonly List<IEnemyMove> _moves;

    public EnemyMoveHistory(params IEnemyMove[] moves) : base(moves)
    {
        _moves = moves.ToList();
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

    public IEnemyMove this[int index] => _moves[index];
    public int Count => _moves.Count;
}