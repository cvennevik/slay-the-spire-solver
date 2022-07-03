using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
            Hand = new Hand(new Strike())
        };
    }

    [TestFixture]
    public class EqualityTests : GameStateTests
    {
        [Test]
        public void TestEquality()
        {
            var gameState1 = CreateBasicGameState();
            var gameState2 = CreateBasicGameState();
            Assert.AreEqual(gameState1, gameState2);
        }
    }

    [TestFixture]
    public class LegalActionTests : GameStateTests
    {
        [Test]
        public void BasicGameState()
        {
            var gameState = CreateBasicGameState();
            var cardActions = gameState.Hand.Cards.SelectMany(x => x.GetLegalActions(gameState));
            var expectedActions = cardActions.Append(new Action(gameState, new EndTurnEffect()));
            AssertLegalActions(gameState, expectedActions.ToArray());
        }

        [Test]
        public void EmptyHand()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand() };
            AssertLegalActions(gameState, new Action(gameState, new EndTurnEffect()));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void OutOfHealth(int amountOfHealth)
        {
            var gameState = CreateBasicGameState() with { PlayerHealth = amountOfHealth };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void NoEnemiesLeft()
        {
            var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void OutOfHealthWithNoEnemies()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = 0,
                EnemyParty = new EnemyParty()
            };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        private static void AssertLegalActions(GameState gameState, params Action[] expectedActions)
        {
            Assert.That(gameState.GetLegalActions(), Is.EquivalentTo(expectedActions));
        }

        private static void AssertNoLegalActions(GameState gameState)
        {
            Assert.IsEmpty(gameState.GetLegalActions());
        }
    }

    [TestFixture]
    public class IsCombatOverTests : GameStateTests
    {
        [Test]
        public void BasicGameState()
        {
            var gameState = CreateBasicGameState();
            Assert.False(gameState.IsCombatOver());
        }

        [Test]
        public void EmptyHand()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand() };
            Assert.False(gameState.IsCombatOver());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void HealthBelowOne(int amountOfHealth)
        {
            var gameState = CreateBasicGameState() with { PlayerHealth = amountOfHealth };
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void NoEnemiesLeft()
        {
            var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void HealthBelowOneWithNoEnemies()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = 0,
                EnemyParty = new EnemyParty()
            };
            Assert.True(gameState.IsCombatOver());
        }
    }
}
