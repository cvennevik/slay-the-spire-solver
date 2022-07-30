using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustCardEffect(Card TargetCard) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        throw new NotImplementedException();
    }
}

[TestFixture]
internal class ExhaustCardEffectTests
{
    [Test]
    public void ExhaustsOneOfTargetCard()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Strike())
        };
        var effect = new ExhaustCardEffect(new Strike());
    }
}