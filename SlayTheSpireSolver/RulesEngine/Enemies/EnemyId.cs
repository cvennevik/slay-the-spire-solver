namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyId
{
    public static readonly EnemyId Default = new();

    public static EnemyId New() => new();

    // Arbitrary string ID makes enemies' ToString() more readable
    private readonly string _printedId;

    private EnemyId()
    {
        _printedId = GeneratePrintedId();
    }

    private static string GeneratePrintedId()
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public override string ToString()
    {
        return $"{_printedId}";
    }
}
