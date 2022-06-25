namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndTurnEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return new[]
        {
            gameState.AsResolvable(new EffectStack(
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new RecoverBaseEnergyEffect(),
                new IncrementTurnEffect(),
                new ResolveAllEnemyMovesEffect(),
                new ClearAllEnemyArmorEffect(),
                new MoveHandToDiscardPileEffect()))
        };
    }
}