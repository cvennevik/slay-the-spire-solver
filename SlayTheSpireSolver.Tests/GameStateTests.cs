using NUnit.Framework;

namespace SlayTheSpireSolver.Tests;

public class GameStateTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
        Assert.NotNull(gameState);
    }
}
