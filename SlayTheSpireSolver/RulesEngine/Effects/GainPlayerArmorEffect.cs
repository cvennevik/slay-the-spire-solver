using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct GainPlayerArmorEffect : IEffect
{
    private readonly Armor _armorGain;

    public GainPlayerArmorEffect(Armor armorGain)
    {
        _armorGain = armorGain;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        return new[] { gameState with { PlayerArmor = gameState.PlayerArmor + _armorGain } };
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var result = ApplyTo(gameState);
        return result.Select(x => new GameStateWithEffectStack(x)).ToList();
    }
}