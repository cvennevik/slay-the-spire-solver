namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class DiscardHandExtension
{
    public static GameState DiscardHand(this GameState gameState)
    {
        return gameState with
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(gameState.DiscardPile.Cards.Concat(gameState.Hand.Cards).ToArray())
        };
    }
}
