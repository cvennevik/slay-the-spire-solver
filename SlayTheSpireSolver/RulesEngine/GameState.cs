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
    public Hand Hand { get; init; } = new();
    public DrawPile DrawPile { get; init; } = new();
    public DiscardPile DiscardPile { get; init; } = new();
    public ExhaustPile ExhaustPile { get; init; } = new();
    public RelicCollection Relics { get; init; } = new();
    public bool CombatHasEnded { get; init; }
    public Turn Turn { get; init; } = 1;
    public EffectStack EffectStack { get; init; } = new();

    public IReadOnlyCollection<PlayerAction> GetLegalActions()
    {
        if (IsCombatOver()) return Array.Empty<PlayerAction>();
        return Hand.Cards
            .SelectMany(card => (IReadOnlyCollection<PlayerAction>)card.GetLegalActions(this))
            .Append(new EndTurnAction(this))
            .ToArray();
    }

    public bool IsCombatOver()
    {
        return PlayerHealth.Amount < 1 || !EnemyParty.Any();
    }

    public GameState ModifyEnemy(EnemyId id, Func<Enemy, Enemy> modifier)
    {
        return this with { EnemyParty = EnemyParty.ModifyEnemy(id, modifier) };
    }

    public (Effect, GameState) PopEffect()
    {
        var (effect, remainingEffectStack) = EffectStack.Pop();
        return (effect, this with { EffectStack = remainingEffectStack });
    }

    public GameState WithAddedEffects(EffectStack effectStack)
    {
        return this with { EffectStack = EffectStack.Push(effectStack) };
    }

    public PossibilitySet Resolve()
    {
        return WithProbability(1).Resolve();
    }

    public Possibility WithProbability(Probability probability)
    {
        return new Possibility(this, probability);
    }

    public override string ToString()
    {
        return $@"GameState {{
    PlayerHealth: {PlayerHealth}
    PlayerArmor: {PlayerArmor}
    BaseEnergy: {BaseEnergy}
    Energy: {Energy}
    EnemyParty: {EnemyParty}
    Hand: {Hand}
    DrawPile: {DrawPile}
    DiscardPile: {DiscardPile}
    ExhaustPile: {ExhaustPile}
    Relics: {Relics}
    CombatHasEnded: {CombatHasEnded}
    Turn: {Turn}
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

        private static void AssertLegalActions(GameState gameState, params PlayerAction[] expectedActions)
        {
            Assert.That(gameState.GetLegalActions(), Is.EquivalentTo(expectedActions));
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
            Assert.AreEqual(new GameState { PlayerArmor = 5 }, resolvedState);
        }

        [Test]
        public void ResolvesTwoEffects()
        {
            var gameState = new GameState
            {
                Energy = 2,
                PlayerArmor = 0,
                EffectStack = new EffectStack(new GainPlayerArmorEffect(5), new RemoveEnergyEffect(1))
            };
            var resolvedState = gameState.Resolve().Single().GameState;
            Assert.AreEqual(new GameState { Energy = 1, PlayerArmor = 5 }, resolvedState);
        }

        [Test]
        public void ResolvesEffectThatAddNewEffects()
        {
            var gameState = new GameState
            {
                PlayerHealth = 30,
                EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() },
                    new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() }),
                EffectStack = new EffectStack(new ResolveForAllEnemiesEffect<ResolveEnemyMoveEffect>())
            };
            var resolvedState = gameState.Resolve().Single().GameState;
            var expectedGameState = gameState with { PlayerHealth = 8, EffectStack = new EffectStack() };
            Assert.AreEqual(expectedGameState, resolvedState);
        }

        [Test]
        public void ResolvesEffectWithMultipleOutcomes()
        {
            var gameState = new GameState
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Defend()),
                EffectStack = new EffectStack(new DrawCardEffect())
            };
            var result = gameState.Resolve();
            var expectedResult1 = new GameState
            {
                Hand = new Hand(new Strike()),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
            };
            var expectedResult2 = new GameState
            {
                Hand = new Hand(new Defend()),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike())
            };
            Assert.AreEqual(2, result.Count());
            Assert.Contains(expectedResult1.WithProbability(0.75), result.ToList());
            Assert.Contains(expectedResult2.WithProbability(0.25), result.ToList());
        }


        [Test]
        public void ResolvesMultipleEffectsWithMultipleOutcomes()
        {
            var gameState = new GameState
            {
                Hand = new Hand(),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Strike(), new Defend()),
                EffectStack = new EffectStack(new DrawCardEffect(), new DrawCardEffect())
            };
            var result = gameState.Resolve();
            var expectedResult1 = new GameState
            {
                Hand = new Hand(new Strike(), new Strike()),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
            }.WithProbability(0.6);
            var expectedResult2 = new GameState
            {
                Hand = new Hand(new Strike(), new Defend()),
                DrawPile = new DrawPile(new Strike(), new Strike(), new Strike())
            }.WithProbability(0.4);
            const double tolerance = 0.000000000000001;
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult1, tolerance)));
            Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult2, tolerance)));
        }
    }
}