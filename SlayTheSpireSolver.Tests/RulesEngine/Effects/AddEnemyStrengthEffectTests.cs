using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddEnemyStrengthEffectTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AddEnemyStrengthEffect(EnemyId.Default, 1),
            new AddEnemyStrengthEffect(EnemyId.Default, 1));
    }
}