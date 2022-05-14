﻿using System.Collections;

namespace SlayTheSpireSolver.Enemies;

public class EnemyParty : IEnumerable<Enemy>
{
    public Enemy GetEnemy(int enemyPosition) => enemies[enemyPosition - 1];

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

    public IEnumerator<Enemy> GetEnumerator()
    {
        return ((IEnumerable<Enemy>)enemies).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return enemies.GetEnumerator();
    }
}
