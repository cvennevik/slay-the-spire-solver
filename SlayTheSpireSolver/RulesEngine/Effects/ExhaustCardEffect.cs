using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ExhaustCardEffect(Card TargetCard) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        throw new NotImplementedException();
    }
}