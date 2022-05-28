using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class ClearEnemyArmorTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState() with
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Armor = new Armor(0), Health = new Health(10) },
                new JawWorm { Armor = new Armor(5), Health = new Health(7) },
                new JawWorm { Armor = new Armor(10), Health = new Health(8) })
        };
        var newGameState = gameState.ClearEnemyArmor();
        var expectedGameState = new GameState() with
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Armor = new Armor(0), Health = new Health(10) },
                new JawWorm { Armor = new Armor(0), Health = new Health(7) },
                new JawWorm { Armor = new Armor(0), Health = new Health(8) })
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}
