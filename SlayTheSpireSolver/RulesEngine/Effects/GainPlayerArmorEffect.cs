using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct GainPlayerArmorEffect : IEffect
{
    private readonly Armor _armorGain;

    public GainPlayerArmorEffect(int armorGainAmount)
    {
        _armorGain = new Armor(Math.Max(armorGainAmount, 0));
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        return new[] { gameState with { PlayerArmor = gameState.PlayerArmor + _armorGain } };
    }

    public IReadOnlyCollection<GameStateWithUnresolvedEffects> Resolve(GameState gameState)
    {
        var result = ApplyTo(gameState);
        return result.Select(x => new GameStateWithUnresolvedEffects(x)).ToList();
    }
}