namespace SlayTheSpireSolver.Enemies;

public class EnemyParty
{
    public int Count => enemies.Length;
    public Enemy this[int index] => enemies[index];

    private readonly Enemy[] enemies;

    public EnemyParty(params Enemy[] enemies)
    {
        this.enemies = enemies;
    }

    public override bool Equals(object? obj)
    {
        var otherParty = obj as EnemyParty;
        if (otherParty == null) return false;
        return enemies.SequenceEqual(otherParty.enemies);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
