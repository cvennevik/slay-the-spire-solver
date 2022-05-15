namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Contains(new DefendCard());
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var oldPlayerArmor = GameState.PlayerArmor;
        var newPlayerArmor = new Armor(oldPlayerArmor.Amount + 5);
        var newHand = GameState.Hand.Remove(new DefendCard());
        return GameState with { PlayerArmor = newPlayerArmor, Hand = newHand };
    }
}
