namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver() && gameState.Hand.Cards.Contains(new DefendCard());
    }

    public DefendAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal Defend action");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        var oldPlayerArmor = GameState.Player.Armor;
        var newPlayerArmor = new Armor(oldPlayerArmor.Value + 5);
        var newPlayer = GameState.Player with { Armor = newPlayerArmor };
        var newHand = GameState.Hand.Remove(new DefendCard());
        return GameState with { Player = newPlayer, Hand = newHand };
    }
}
