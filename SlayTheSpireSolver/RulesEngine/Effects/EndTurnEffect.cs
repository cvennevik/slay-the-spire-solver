namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndTurnEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        return new[]
        {
            gameState.WithEffectStack(new EffectStack(
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new DrawCardEffect(),
                new RecoverBaseEnergyEffect(),
                new IncrementTurnEffect(),
                new ResolveAllEnemyMovesEffect(),
                new ClearAllEnemyArmorEffect()))
        };
    }
}