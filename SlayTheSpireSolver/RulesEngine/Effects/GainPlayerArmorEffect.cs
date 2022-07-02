using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GainPlayerArmorEffect : Effect
{
    private readonly Armor _armorGain;

    public GainPlayerArmorEffect(Armor armorGain)
    {
        _armorGain = armorGain;
    }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { PlayerArmor = gameState.PlayerArmor + _armorGain };
    }
}