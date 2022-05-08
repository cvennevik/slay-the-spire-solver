namespace SlayTheSpireSolver;

public class GameState
{
    public IReadOnlyList<Enemy> Enemies { get; init; } = new List<Enemy>();

    public override bool Equals(object? obj)
    {
        var gameState = obj as GameState;
        if (obj == null)
        {
            return false;
        }

        return Enemies.SequenceEqual(gameState.Enemies);
    }

    public override int GetHashCode()
    {
        // Uhhh let's hope I never need this lol
        return 0;
    }
}
