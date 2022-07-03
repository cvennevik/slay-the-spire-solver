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
    public void BasicGameState()
    {
        var gameState = new GameState()
        {
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
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