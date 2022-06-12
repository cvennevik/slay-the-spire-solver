using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = 70,
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
            Hand = new Hand(new Strike()),
            Turn = new Turn(1)
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
            var strike = new Strike();
            var playStrikeAction = new ActionWithEffectStack(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(strike),
                strike.GetEffect(gameState),
                new RemoveCardFromHandEffect(strike),
                new RemoveEnergyEffect(strike.GetCost())));
            AssertLegalActions(gameState, playStrikeAction, new EndTurnAction(gameState));
        }

        [Test]
        public void EmptyHand()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand() };
            AssertLegalActions(gameState, new EndTurnAction(gameState));
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

        private static void AssertLegalActions(GameState gameState, params IAction[] expectedActions)
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
