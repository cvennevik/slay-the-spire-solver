namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    private static readonly Energy EnergyCost = new(1);
    private static readonly Armor ArmorGain = new(5);

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver()
            && gameState.Hand.Contains(new DefendCard())
            && gameState.Energy >= EnergyCost;
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        return GameState
            .Remove(EnergyCost)
            .MoveCardFromHandToDiscardPile(new DefendCard()) with
        {
            PlayerArmor = GameState.PlayerArmor + ArmorGain
        };
    }
}
