namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    public DefendAction(GameState gameState)
    {
        if (!gameState.EnemyParty.Any()) throw new ArgumentException("Cannot play Defend when no enemies remain");
        if (!gameState.Hand.Cards.Contains(new DefendCard())) throw new ArgumentException("No Defend card in hand");
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
