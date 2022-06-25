using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct GainPlayerArmorEffect : IEffect
{
    private readonly Armor _armorGain;

    public GainPlayerArmorEffect(Armor armorGain)
    {
        _armorGain = armorGain;
    }

    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var result = new[] { gameState with { PlayerArmor = gameState.PlayerArmor + _armorGain } };
        return result.Select(x => new ResolvableGameState(x)).ToList();
    }
}