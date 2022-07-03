using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayTargetedCardAction : PlayCardAction
{
    public PlayTargetedCardAction(GameState gameState, EffectStack effectStack) : base(
        new ResolvableGameState(gameState, effectStack)) { }

    public PlayTargetedCardAction(GameState gameState, TargetedCard card, EnemyId target)
        : base(new ResolvableGameState(gameState, new EffectStack())) { }
}