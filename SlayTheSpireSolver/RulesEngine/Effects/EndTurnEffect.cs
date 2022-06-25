namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndTurnEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState.WithEffects(new EffectStack(
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new RecoverBaseEnergyEffect(),
            new IncrementTurnEffect(),
            new ResolveAllEnemyMovesEffect(),
            new ClearAllEnemyArmorEffect(),
            new MoveHandToDiscardPileEffect()));
    }
}