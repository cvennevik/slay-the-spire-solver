namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyId
{
    public static readonly EnemyId Default = new();

    private EnemyId() { }

    public static EnemyId New() => new();
}
