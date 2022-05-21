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
public class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new StrikeCard()),
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
            AssertLegalActions(gameState, new StrikeAction(gameState), new EndTurnAction(gameState));
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
            var gameState = CreateBasicGameState() with { PlayerHealth = new Health(amountOfHealth) };
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
                PlayerHealth = new Health(0),
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
            var gameState = CreateBasicGameState() with { PlayerHealth = new Health(amountOfHealth) };
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
                PlayerHealth = new Health(0),
                EnemyParty = new EnemyParty()
            };
            Assert.True(gameState.IsCombatOver());
        }
    }

    [TestFixture]
    public class MoveCardFromHandToDiscardPileTests : GameStateTests
    {
        [Test]
        public void Test1()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new StrikeCard()),
                DiscardPile = new DiscardPile()
            };
            var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
            var expectedGameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DiscardPile = new DiscardPile(new StrikeCard())
            };
            Assert.AreEqual(expectedGameState, newGameState);
        }

        [Test]
        public void Test2()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new StrikeCard()),
                DiscardPile = new DiscardPile(new StrikeCard())
            };
            var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
            var expectedGameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DiscardPile = new DiscardPile(new StrikeCard(), new StrikeCard())
            };
            Assert.AreEqual(expectedGameState, newGameState);
        }

        [Test]
        public void Test3()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand(new StrikeCard()) };
            Assert.Throws<ArgumentException>(() => gameState.MoveCardFromHandToDiscardPile(new DefendCard()));
        }
    }

    [TestFixture]
    public class RemoveEnergyTests : GameStateTests
    {
        [Test]
        [TestCase(3, 2, 1)]
        [TestCase(3, 0, 3)]
        [TestCase(3, 3, 0)]
        [TestCase(3, 4, 0)]
        [TestCase(0, 0, 0)]
        public void Test(int initialAmount, int amountToRemove, int expectedAmount)
        {
            var gameState = CreateBasicGameState() with { Energy = new Energy(initialAmount) };
            var newGameState = gameState.Remove(new Energy(amountToRemove));
            var expectedGameState = CreateBasicGameState() with { Energy = new Energy(expectedAmount) };
            Assert.AreEqual(expectedGameState, newGameState);
        }
    }

    [TestFixture]
    public class DiscardHandTests : GameStateTests
    {
        [Test]
        public void Test1()
        {
            var firstGameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DiscardPile = new DiscardPile()
            };
            var nextGameState = firstGameState.DiscardHand();
            var expectedGameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DiscardPile = new DiscardPile(new StrikeCard())
            };
            Assert.AreEqual(expectedGameState, nextGameState);
        }

        [Test]
        public void Test2()
        {
            var firstGameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DiscardPile = new DiscardPile(new DefendCard())
            };
            var nextGameState = firstGameState.DiscardHand();
            var expectedGameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DiscardPile = new DiscardPile(new DefendCard(), new StrikeCard())
            };
            Assert.AreEqual(expectedGameState, nextGameState);
        }

        [Test]
        public void Test3()
        {
            var firstGameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DiscardPile = new DiscardPile(new DefendCard())
            };
            var nextGameState = firstGameState.DiscardHand();
            Assert.AreEqual(firstGameState, nextGameState);
        }

        [Test]
        public void Test4()
        {
            var firstGameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DiscardPile = new DiscardPile()
            };
            var nextGameState = firstGameState.DiscardHand();
            Assert.AreEqual(firstGameState, nextGameState);
        }
    }

    [TestFixture]
    public class DrawCardTests : GameStateTests
    {
        [Test]
        public void DoesNotDrawCardWhenDrawPileAndDiscardPileEmpty()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile()
            };
            var resolvedStates = gameState.DrawCard();
            Assert.AreEqual(gameState, resolvedStates.Single());
        }

        [Test]
        public void DrawsCardWhenOneCardInDrawPile()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new StrikeCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile()
            };
            Assert.AreEqual(expectedState, resolvedStates.Single());
        }

        [Test]
        public void CanDrawEitherCardWhenTwoDifferentCardsInDrawPile()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new StrikeCard(), new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState1 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(new DefendCard())
            };
            var expectedState2 = CreateBasicGameState() with
            {
                Hand = new Hand(new DefendCard()),
                DrawPile = new DrawPile(new StrikeCard())
            };
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.Contains(expectedState1, resolvedStates.ToList());
            Assert.Contains(expectedState2, resolvedStates.ToList());
        }

        [Test]
        public void OnlyTwoPossibleStatesWhenTwoCardKindsInDrawPile()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                    new DefendCard(), new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState1 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(),
                    new DefendCard(), new DefendCard())
            };
            var expectedState2 = CreateBasicGameState() with
            {
                Hand = new Hand(new DefendCard()),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                    new DefendCard())
            };
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.Contains(expectedState1, resolvedStates.ToList());
            Assert.Contains(expectedState2, resolvedStates.ToList());
        }

        [Test]
        public void CanOnlyDrawOneCardWhenSameCardsInDrawPile()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new StrikeCard(), new StrikeCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(new StrikeCard())
            };
            Assert.AreEqual(expectedState, resolvedStates.Single());
        }

        [Test]
        public void AddsCardsToExistingHandInSinglePossibleState()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new DefendCard()),
                DrawPile = new DrawPile()
            };
            Assert.AreEqual(expectedState, resolvedStates.Single());
        }

        [Test]
        public void AddsCardsToExistingHandInAllPossibleStates()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(new StrikeCard(), new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState1 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new StrikeCard()),
                DrawPile = new DrawPile(new DefendCard())
            };
            var expectedState2 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(),new DefendCard()),
                DrawPile = new DrawPile(new StrikeCard())
            };
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.Contains(expectedState1, resolvedStates.ToList());
            Assert.Contains(expectedState2, resolvedStates.ToList());
        }

        [Test]
        public void ShufflesAndDrawsDiscardPileWithSingleCard()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(),
                DiscardPile = new DiscardPile(new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new DefendCard()),
                DrawPile = new DrawPile(),
                DiscardPile = new DiscardPile()
            };
            Assert.AreEqual(expectedState, resolvedStates.Single());
        }

        [Test]
        public void ShufflesAndDrawsDiscardPileWithMultipleCards()
        {
            var gameState = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard()),
                DrawPile = new DrawPile(),
                DiscardPile = new DiscardPile(new StrikeCard(), new DefendCard(), new DefendCard())
            };
            var resolvedStates = gameState.DrawCard();
            var expectedState1 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new StrikeCard()),
                DrawPile = new DrawPile(new DefendCard(), new DefendCard()),
                DiscardPile = new DiscardPile()
            };
            var expectedState2 = CreateBasicGameState() with
            {
                Hand = new Hand(new StrikeCard(), new DefendCard()),
                DrawPile = new DrawPile(new StrikeCard(), new DefendCard()),
                DiscardPile = new DiscardPile()
            };
            Assert.AreEqual(2, resolvedStates.Count);
            Assert.Contains(expectedState1, resolvedStates.ToList());
            Assert.Contains(expectedState2, resolvedStates.ToList());
        }
    }
}
