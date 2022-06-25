using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class EndTurnActionTests
{
    [TestFixture]
    public class ResolveTests : EndTurnActionTests
    {
        [Test]
        public void EnemyAttacks()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = 50,
                EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() })
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Health(38), resolvedStates.Single().PlayerHealth);
        }

        [Test]
        public void ClearsEnemyArmor()
        {
            var gameState = CreateBasicGameState() with
            {
                EnemyParty = new EnemyParty(new JawWorm { Armor = 10 })
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Armor(0), resolvedStates.Single().EnemyParty.First().Armor);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void TurnNumberIncreases(int initialTurn, int expectedTurn)
        {
            var gameState = CreateBasicGameState() with { Turn = initialTurn };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Turn(expectedTurn), resolvedStates.Single().Turn);
        }

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards1()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new Strike(), new Defend()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile()
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Hand(new Strike(), new Defend()), resolvedStates.Single().Hand);
            Assert.AreEqual(new DiscardPile(), resolvedStates.Single().DiscardPile);
            Assert.AreEqual(new DrawPile(), resolvedStates.Single().DrawPile);
        }

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards2()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new Defend()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                    new Strike(), new Strike(), new Strike())
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(new Hand(new Strike(), new Strike(), new Strike(), new Strike(), new Strike()),
                resolvedStates.Single().Hand);
            Assert.AreEqual(new DiscardPile(new Defend()), resolvedStates.Single().DiscardPile);
            Assert.AreEqual(new DrawPile(new Strike()), resolvedStates.Single().DrawPile);
        }

        [Test]
        public void DiscardsHandAndDrawsFiveNewCards3()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new Defend(), new Strike()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Strike())
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var resolvedStates = endTurnAction.ResolveToPossibleStates();
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.AreEqual(1, resolvedStates.Count(x =>
                x.Hand == new Hand(new Strike(), new Strike(), new Strike(), new Strike(), new Strike()) &&
                x.DrawPile == new DrawPile(new Defend()) &&
                x.DiscardPile == new DiscardPile()));
            Assert.AreEqual(1, resolvedStates.Count(x =>
                x.Hand == new Hand(new Strike(), new Strike(), new Strike(), new Strike(), new Defend()) &&
                x.DrawPile == new DrawPile(new Strike()) &&
                x.DiscardPile == new DiscardPile()));
        }

        [Test]
        public void RecoversBaseEnergy()
        {
            var gameState = CreateBasicGameState() with
            {
                BaseEnergy = 4,
                Energy = 0
            };
            var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
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
            var action1 = new ActionWithEffectStack(gameState, new EndTurnEffect());
            var action2 = new ActionWithEffectStack(gameState, new EndTurnEffect());
            Assert.AreEqual(action1, action2);
        }

        [Test]
        public void TestEquality2()
        {
            var action1 = new ActionWithEffectStack(CreateBasicGameState(), new EndTurnEffect());
            var action2 = new ActionWithEffectStack(CreateBasicGameState(), new EndTurnEffect());
            Assert.AreEqual(action1, action2);
        }

        [Test]
        public void TestEquality3()
        {
            var action1 = new ActionWithEffectStack(CreateBasicGameState(), new EndTurnEffect());
            var action2 = new ActionWithEffectStack(CreateBasicGameState() with { Turn = 2 }, new EndTurnEffect());
            Assert.AreNotEqual(action1, action2);
        }
    }

    private static GameState CreateBasicGameState()
    {
        return new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() }),
            PlayerHealth = 50
        };
    }
}
