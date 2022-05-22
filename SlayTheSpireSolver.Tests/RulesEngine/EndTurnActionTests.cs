using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class EndTurnActionTests
{
    [TestFixture]
    public class LegalityTests : EndTurnActionTests
    {
        [Test]
        public void CannotEndTurnAfterWinning()
        {
            var gameState = new GameState();
            Assert.Throws<ArgumentException>(() => new EndTurnAction(gameState));
        }

        [Test]
        public void CannotEndTurnWhenDefeated()
        {
            var gameState = new GameState
            {
                PlayerHealth = new Health(0),
                EnemyParty = new EnemyParty(new JawWorm())
            };
            Assert.Throws<ArgumentException>(() => new EndTurnAction(gameState));
        }
    }

    [TestFixture]
    public class ResolveTests : EndTurnActionTests
    {
        [Test]
        public void EnemyAttacks()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = new Health(50),
                EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() })
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Health(38), resolvedStates.Single().PlayerHealth);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void TurnNumberIncreases(int initialTurnNumber, int expectedTurnNumber)
        {
            var gameState = CreateBasicGameState() with { Turn = new Turn(initialTurnNumber) };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Turn(expectedTurnNumber), resolvedStates.Single().Turn);
        }
    }

    [TestFixture]
    public class EqualityTests : EndTurnActionTests
    {
        [Test]
        public void TestEquality1()
        {
            var gameState = CreateBasicGameState();
            var action1 = new EndTurnAction(gameState);
            var action2 = new EndTurnAction(gameState);
            Assert.AreEqual(action1, action2);
        }

        [Test]
        public void TestEquality2()
        {
            var action1 = new EndTurnAction(CreateBasicGameState());
            var action2 = new EndTurnAction(CreateBasicGameState());
            Assert.AreEqual(action1, action2);
        }

        [Test]
        public void TestEquality3()
        {
            var action1 = new EndTurnAction(CreateBasicGameState());
            var action2 = new EndTurnAction(CreateBasicGameState() with { Turn = new Turn(2) });
            Assert.AreNotEqual(action1, action2);
        }
    }

    private static GameState CreateBasicGameState()
    {
        return new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() }),
            PlayerHealth = new Health(50)
        };
    }
}
