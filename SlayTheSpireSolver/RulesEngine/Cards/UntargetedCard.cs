using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record UntargetedCard : Card
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
public class UntargetedCardTests
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