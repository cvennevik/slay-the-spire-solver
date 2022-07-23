namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndTurnEffect : Effect
{
    public virtual PossibilitySet Resolve(GameState gameState)
    {
        return gameState.WithAddedEffects(new EffectStack(
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new ClearPlayerArmorEffect(),
            new RecoverBaseEnergyEffect(),
            new IncrementTurnEffect(),
            new ResolveForAllEnemiesEffect<ChooseNewEnemyMoveEffect>(),
            new ResolveForAllEnemiesEffect<ResolveEnemyMoveEffect>(),
            new ClearAllEnemyArmorEffect(),
            new MoveHandToDiscardPileEffect()));
    }
}