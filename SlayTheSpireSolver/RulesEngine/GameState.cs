using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameState
{
    public Health PlayerHealth { get; init; } = 1;
    public Armor PlayerArmor { get; init; } = 0;
    public Energy BaseEnergy { get; init; } = 3;
    public Energy Energy { get; init; } = 0;
    public EnemyParty EnemyParty { get; init; } = new();
    public Turn Turn { get; init; } = 1;
    public Hand Hand { get; init; } = new();
    public DrawPile DrawPile { get; init; } = new();
    public DiscardPile DiscardPile { get; init; } = new();
    public EffectStack EffectStack { get; init; } = new();

    public IReadOnlyCollection<PlayerAction> GetLegalActions()
    {
        var legalActions = new List<PlayerAction>();
        legalActions.AddRange(Hand.Cards.SelectMany(card => card.GetLegalActions(this)));
        if (!IsCombatOver())
        {
            legalActions.Add(new EndTurnAction(this));
        }
        return legalActions;
    }

    public bool IsCombatOver()
    {
        return PlayerHealth.Amount < 1 || !EnemyParty.Any();
    }

    public GameState ModifyEnemy(EnemyId id, Func<Enemy, Enemy> modifier)
    {
        return this with { EnemyParty = EnemyParty.ModifyEnemy(id, modifier) };
    }

    public ResolvableGameState WithEffects(params Effect[] effects) => WithEffects(new EffectStack(effects));
    public ResolvableGameState WithEffects(EffectStack? effectStack = null)
    {
        return new ResolvableGameState(this with { EffectStack = effectStack ?? new EffectStack() });
    }

    public IReadOnlyList<Possibility> Resolve()
    {
        return new ResolvableGameState(this).WithProbability(1).Resolve();
    }

    public Possibility WithProbability(Probability probability) => new(this, probability);

    public override string ToString()
    {
        return $@"GameState {{
    PlayerHealth: {PlayerHealth}
    PlayerArmor: {PlayerArmor}
    BaseEnergy: {BaseEnergy}
    Energy: {Energy}
    EnemyParty: {EnemyParty}
    Turn: {Turn}
    Hand: {Hand}
    DrawPile: {DrawPile}
    DiscardPile: {DiscardPile}
    EffectStack: {EffectStack}
}}";
    }
}

[TestFixture]
internal class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new GameState
        {
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
            Hand = new Hand(new Strike())
        };
    }

    [TestFixture]
    internal class LegalActionTests : GameStateTests
    {
        [Test]
        public void BasicGameState()
        {
            var gameState = CreateBasicGameState();
            var expectedActions = new PlayerAction[]
            {
                new Strike().GetLegalActions(gameState).Single(),
                new EndTurnAction(gameState)
            };
            AssertLegalActions(gameState, expectedActions.ToArray());
        }

        [Test]
        public void EmptyHand()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand() };
            AssertLegalActions(gameState, new EndTurnAction(gameState));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void OutOfHealth(int amountOfHealth)
        {
            var gameState = CreateBasicGameState() with { PlayerHealth = amountOfHealth };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void NoEnemiesLeft()
        {
            var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void OutOfHealthWithNoEnemies()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = 0,
                EnemyParty = new EnemyParty()
            };
            AssertNoLegalActions(gameState);
            Assert.True(gameState.IsCombatOver());
        }

        private static void AssertLegalActions(GameState gameState, params PlayerAction[] expectedActions)
        {
            Assert.That(gameState.GetLegalActions(), Is.EquivalentTo(expectedActions));
        }

        private static void AssertNoLegalActions(GameState gameState)
        {
            Assert.IsEmpty(gameState.GetLegalActions());
        }
    }

    [TestFixture]
    internal class IsCombatOverTests : GameStateTests
    {
        [Test]
        public void BasicGameState()
        {
            var gameState = CreateBasicGameState();
            Assert.False(gameState.IsCombatOver());
        }

        [Test]
        public void EmptyHand()
        {
            var gameState = CreateBasicGameState() with { Hand = new Hand() };
            Assert.False(gameState.IsCombatOver());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void HealthBelowOne(int amountOfHealth)
        {
            var gameState = CreateBasicGameState() with { PlayerHealth = amountOfHealth };
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void NoEnemiesLeft()
        {
            var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
            Assert.True(gameState.IsCombatOver());
        }

        [Test]
        public void HealthBelowOneWithNoEnemies()
        {
            var gameState = CreateBasicGameState() with
            {
                PlayerHealth = 0,
                EnemyParty = new EnemyParty()
            };
            Assert.True(gameState.IsCombatOver());
        }
    }

    [TestFixture]
    internal class ResolveTests : GameStateTests
    {
        [Test]
        public void ResolvesZeroEffects()
        {
            var gameState = new GameState();
            var resolvedState = gameState.Resolve().Single().GameState;
            Assert.AreEqual(new GameState(), resolvedState);
        }

        
        [Test]
        public void ResolvesOneEffect()
        {
            var gameState = new GameState { PlayerArmor = 0, EffectStack = new[] { new GainPlayerArmorEffect(5) } };
            var resolvedState = gameState.Resolve().Single().GameState;
        }
    }
}
