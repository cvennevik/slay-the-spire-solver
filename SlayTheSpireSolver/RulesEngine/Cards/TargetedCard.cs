using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new Action(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(gameState),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<Action>();
    }
}

[TestFixture]
public class TargetedCardTests
{
    [Test]
    public void TestGetLegalActions()
    {
        var gameState = new GameState
        {
            Energy = 1,
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new Strike())
        };
        var strike = new Strike();
        var playStrikeAction = new Action(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(strike),
            strike.GetEffect(gameState),
            new RemoveCardFromHandEffect(strike),
            new RemoveEnergyEffect(strike.GetCost())));
        Assert.AreEqual(playStrikeAction, strike.GetLegalActions(gameState).Single());
    }
}

internal class TargetedCardTests<TCard> : CommonCardTests<TCard> where TCard : TargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(_basicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(_card),
            _card.GetEffect(_basicGameState),
            new RemoveCardFromHandEffect(_card),
            new RemoveEnergyEffect(_card.GetCost())));
        Assert.AreEqual(expectedAction, _card.GetLegalActions(_basicGameState).Single());
    }
}