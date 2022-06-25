using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct GainPlayerArmorEffect : IEffect
{
    private readonly Armor _armorGain;

    public GainPlayerArmorEffect(Armor armorGain)
    {
        _armorGain = armorGain;
    }

    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { PlayerArmor = gameState.PlayerArmor + _armorGain };
    }
}