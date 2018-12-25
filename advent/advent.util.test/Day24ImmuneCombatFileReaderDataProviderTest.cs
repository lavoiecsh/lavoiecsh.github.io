using System.Collections.Generic;
using System.Linq;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day24ImmuneCombatFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsImmuneCombatInFile()
        {
            const string filename = "data\\day24_immune_combat.txt";
            var dataProdiver = new Day24ImmuneCombatFileReaderDataProvider(filename);
            var expectedImmuneSystems = new List<Day24Solver.Group>
            {
                new Day24Solver.Group(17, 5390, 4507, "fire", 2),
                new Day24Solver.Group(989, 1274, 25, "slashing", 3)
            };
            expectedImmuneSystems[0].Weaknesses.Add("radiation");
            expectedImmuneSystems[0].Weaknesses.Add("bludgeoning");
            expectedImmuneSystems[1].Immunities.Add("fire");
            expectedImmuneSystems[1].Weaknesses.Add("bludgeoning");
            expectedImmuneSystems[1].Weaknesses.Add("slashing");
            var expectedInfections = new List<Day24Solver.Group>
            {
                new Day24Solver.Group(801, 4706, 116, "bludgeoning", 1),
                new Day24Solver.Group(4485, 2961, 12, "slashing", 4)
            };
            expectedInfections[0].Weaknesses.Add("radiation");
            expectedInfections[1].Immunities.Add("radiation");
            expectedInfections[1].Weaknesses.Add("fire");
            expectedInfections[1].Weaknesses.Add("cold");

            var combat = dataProdiver.GetData();
            Assert.Equal(expectedImmuneSystems, combat.ImmuneSystems, new GroupComparer());
            Assert.Equal(expectedInfections, combat.Infections, new GroupComparer());
        }
        
        private class GroupComparer : IEqualityComparer<Day24Solver.Group>
        {
            public bool Equals(Day24Solver.Group x, Day24Solver.Group y)
            {
                return x.Units == y.Units &&
                       x.HP == y.HP &&
                       x.Damage == y.Damage &&
                       x.DamageType == y.DamageType &&
                       x.Initiative == y.Initiative &&
                       x.Immunities.SequenceEqual(y.Immunities) &&
                       x.Weaknesses.SequenceEqual(y.Weaknesses);
            }

            public int GetHashCode(Day24Solver.Group obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}