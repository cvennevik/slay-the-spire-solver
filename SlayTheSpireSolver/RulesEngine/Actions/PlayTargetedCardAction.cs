using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayTargetedCardAction(GameState GameState, TargetedCard TargetedCard, EnemyId Target) : PlayCardAction
{
    public Card Card => TargetedCard;

    public PossibilitySet Resolve()
    {
        var unresolvedGameState = GameState with
        {
            EffectStack = new EffectStack(new AddCardToDiscardPileEffect(TargetedCard))
                .Push(TargetedCard.GetTargetedEffects(Target))
                .Push(new RemoveCardFromHandEffect(TargetedCard))
                .Push(new RemoveEnergyEffect(TargetedCard.GetCost()))
        };
        return unresolvedGameState.Resolve();
    }
}

[TestFixture]
internal class PlayTargetedCardActionTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState
        {
            Energy = 3,
            Hand = new Hand(new Strike()),
            EnemyParty = new[] { new JawWorm { Health = 7 } }
        };
        var action = new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default);
        var result = action.Resolve().Single().GameState;
        var expectedGameState = new GameState
        {
            Energy = 2,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike()),
            EnemyParty = new[] { new JawWorm { Health = 1 } }
        };
        Assert.AreEqual(expectedGameState, result);
    }
}