using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;

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
        
    }
}