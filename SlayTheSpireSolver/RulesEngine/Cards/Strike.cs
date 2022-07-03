﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : Card
{
    private static readonly Energy Cost = 1;

    public override Energy GetCost() => Cost;
    public override Effect GetEffect(GameState gameState) => new AttackEnemyEffect(gameState.EnemyParty.First().Id, 6);

    public override string ToString() => "Strike";
}


[TestFixture]
internal class StrikeTests : CommonCardTests<Strike> { }