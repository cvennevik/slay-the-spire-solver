﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : Card
{
    private static readonly Effect Effect = new GainPlayerArmorEffect(new Armor(5));

    public override Energy GetCost() => 1;
    public override Effect GetEffect(GameState gameState) => Effect;

    public override string ToString() => "Defend";
}

[TestFixture]
internal class DefendTests : CommonCardTests<Defend> { }