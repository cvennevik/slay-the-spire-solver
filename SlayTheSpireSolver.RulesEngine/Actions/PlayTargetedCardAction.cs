using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayTargetedCardAction : PlayCardAction
{
    public PlayTargetedCardAction(GameState gameState, TargetedCard card, EnemyId target)
        : base(gameState, card, card.GetTargetedEffects(target))
    {
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
            EnemyParty = new[] { new JawWorm { Health = new Health(7, 7) } }
        };
        var action = new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default);
        var result = action.Resolve().Single().GameState;
        var expectedGameState = new GameState
        {
            Energy = 2,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike()),
            EnemyParty = new[] { new JawWorm { Health = new Health(1, 7) } }
        };
        Assert.AreEqual(expectedGameState, result);
    }
}