using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayTargetedCardAction : PlayCardAction
{
    public PlayTargetedCardAction(GameState gameState, TargetedCard card, EnemyId target)
        : base(gameState, card, card.GetTargetedEffects(target)) { }
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
    }
}