using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.DrawPile.Cards.Any() && gameState.DiscardPile.Cards.Any())
            gameState = gameState with
            {
                DiscardPile = new DiscardPile(),
                DrawPile = new DrawPile(gameState.DiscardPile.Cards.ToArray())
            };

        if (gameState.DrawPile.Cards.Any())
        {
            var drawPileCount = gameState.DrawPile.Cards.Count;
            var cardDrawProbabilities = new Dictionary<Card, double>();
            var fractionOfDrawPilePerCard = 1.0 / drawPileCount;
            foreach (var card in gameState.DrawPile.Cards)
                if (cardDrawProbabilities.ContainsKey(card)) cardDrawProbabilities[card] += fractionOfDrawPilePerCard;
                else cardDrawProbabilities[card] = fractionOfDrawPilePerCard;

            var uniqueCards = cardDrawProbabilities.Keys.ToArray();
            var newResults = new Possibility[uniqueCards.Length];
            for (var i = 0; i < uniqueCards.Length; i++)
            {
                var card = uniqueCards[i];
                var newGameState = gameState with
                {
                    Hand = gameState.Hand.Add(card),
                    DrawPile = gameState.DrawPile.Remove(card)
                };
                var probability = new Probability(cardDrawProbabilities[card]);
                newResults[i] = new Possibility(newGameState, probability);
            }

            return newResults;
        }

        return gameState;
    }
}

[TestFixture]
internal class DrawCardEffectTests
{
    [Test]
    public void DoesNothingWhenDrawPileAndDiscardPileEmpty()
    {
        var gameState = new GameState { Hand = new Hand(), DrawPile = new DrawPile(), DiscardPile = new DiscardPile() };
        var result = new DrawCardEffect().Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DrawsCardWhenSingleCardInDrawPile()
    {
        var gameState = new GameState { Hand = new Hand(), DrawPile = new DrawPile(new Strike()) };
        var result = new DrawCardEffect().Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState { Hand = new Hand(new Strike()), DrawPile = new DrawPile() };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void DrawsEitherCardWhenTwoCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(), DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedGameState1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var expectedGameState2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, result.Count());
        Assert.Contains(expectedGameState1.WithProbability(0.5), result.ToList());
        Assert.Contains(expectedGameState2.WithProbability(0.5), result.ToList());
    }

    [Test]
    public void DrawsEitherKindWhenTwoCardKindsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedResult1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike(),
                new Defend(), new Defend())
        }.WithProbability(0.6);
        var expectedResult2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend())
        }.WithProbability(0.4);
        const double tolerance = 0.000000000000001;
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult1, tolerance)));
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult2, tolerance)));
    }

    [Test]
    public void CanOnlyDrawOneCardWhenSameCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var result = new DrawCardEffect().Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsCardsToExistingHandInSinglePossibleState()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile()
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsCardsToExistingHandInAllPossibleStates()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedGameState1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var expectedGameState2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, result.Count());
        Assert.Contains(expectedGameState1.WithProbability(0.5), result.ToList());
        Assert.Contains(expectedGameState2.WithProbability(0.5), result.ToList());
    }

    [Test]
    public void ShufflesAndDrawsDiscardPileWithSingleCard()
    {
        var gameState = new GameState { Hand = new Hand(), DiscardPile = new DiscardPile(new Strike()) };
        var result = new DrawCardEffect().Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState { Hand = new Hand(new Strike()), DiscardPile = new DiscardPile() };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void ShufflesAndDrawsDiscardPileWithMultipleCards()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Defend(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedState1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend()),
            DiscardPile = new DiscardPile()
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike(), new Defend(), new Defend()),
            DiscardPile = new DiscardPile()
        };
        Assert.AreEqual(2, result.Count());
        Assert.Contains(expectedState1.WithProbability(0.25), result.ToList());
        Assert.Contains(expectedState2.WithProbability(0.75), result.ToList());
    }
}