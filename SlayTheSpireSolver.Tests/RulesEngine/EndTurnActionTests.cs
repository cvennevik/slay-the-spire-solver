using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
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
        public void ClearsEnemyArmor()
        {
            var gameState = CreateBasicGameState() with
            {
                EnemyParty = new EnemyParty(new JawWorm { Armor = new Armor(10)})
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Armor(0), resolvedStates.Single().EnemyParty.First().Armor);
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

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards1()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new DefendCard()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile()
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Hand(new StrikeCard(), new DefendCard()), resolvedStates.Single().Hand);
            Assert.AreEqual(new DiscardPile(), resolvedStates.Single().DiscardPile);
            Assert.AreEqual(new DrawPile(), resolvedStates.Single().DrawPile);
        }

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards2()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new DefendCard()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                    new StrikeCard(), new StrikeCard(), new StrikeCard())
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Hand(new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard()),
                resolvedStates.Single().Hand);
            Assert.AreEqual(new DiscardPile(new DefendCard()), resolvedStates.Single().DiscardPile);
            Assert.AreEqual(new DrawPile(new StrikeCard()), resolvedStates.Single().DrawPile);
        }

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards3()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new DefendCard(), new StrikeCard()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard())
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.AreEqual(1, resolvedStates.Count(x =>
                x.Hand == new Hand(new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard()) &&
                x.DrawPile == new DrawPile(new DefendCard()) &&
                x.DiscardPile == new DiscardPile()));
            Assert.AreEqual(1, resolvedStates.Count(x =>
                x.Hand == new Hand(new StrikeCard(), new StrikeCard(), new StrikeCard(), new StrikeCard(), new DefendCard()) &&
                x.DrawPile == new DrawPile(new StrikeCard()) &&
                x.DiscardPile == new DiscardPile()));
        }

        [Test]
        public void RecoversBaseEnergy()
        {
            var gameState = CreateBasicGameState() with
            {
                BaseEnergy = new Energy(4),
                Energy = new Energy(0)
            };
            var endTurnAction = new EndTurnAction(gameState);
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Energy(4), resolvedStates.Single().Energy);
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
