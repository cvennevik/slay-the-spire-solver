using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

public class CommonCardTestsBase<TCard> where TCard : ICard, new()
{
    private readonly TCard _card;
    private readonly GameState _basicGameState;

    protected CommonCardTestsBase()
    {
        _card = new TCard();
        _basicGameState = new GameState
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(_card),
            DiscardPile = new DiscardPile(),
        };
    }

    [Test]
    public void InstancesAreEqual()
    {
        Assert.AreEqual(new TCard(), new TCard());
    }

    [Test]
    public void MustHaveEnemies()
    {
        AssertNoLegalActions(_basicGameState with { EnemyParty = new EnemyParty() });
    }

    [Test]
    public void MustHaveCardInHand()
    {
        AssertNoLegalActions(_basicGameState with { Hand = new Hand() });
    }
        
    [Test]
    public void MustBeAlive()
    {
        AssertNoLegalActions(_basicGameState with { PlayerHealth = new Health(0) });
    }

    [Test]
    public void MustHaveEnoughEnergy()
    {
        AssertNoLegalActions(_basicGameState with { Energy = new Energy(0) });
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var legalActions = PlayCardAction.GetLegalActions(_basicGameState, _card);
        Assert.AreEqual(new PlayCardAction(_basicGameState, _card), legalActions.Single());
    }

    private void AssertNoLegalActions(GameState gameState)
    {
        Assert.IsEmpty(PlayCardAction.GetLegalActions(gameState, _card));
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, _card));
    }
}