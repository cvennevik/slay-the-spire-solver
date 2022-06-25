using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction(GameState GameState) : ActionWithEffectStack(GameState,
    new EffectStack(new EndTurnEffect()));
