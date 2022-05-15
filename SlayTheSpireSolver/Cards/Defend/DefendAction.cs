namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    private const int EnergyCost = 1;

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Contains(new DefendCard()) && gameState.Energy.Amount >= EnergyCost;
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var newPlayerArmor = new Armor(GameState.PlayerArmor.Amount + 5);
        var newEnergy = new Energy(GameState.Energy.Amount - EnergyCost);
        var newHand = GameState.Hand.Remove(new DefendCard());
        return GameState with { PlayerArmor = newPlayerArmor, Energy = newEnergy, Hand = newHand };
    }
}
