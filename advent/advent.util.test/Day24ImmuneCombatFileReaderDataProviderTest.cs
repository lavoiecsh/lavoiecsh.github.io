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
            var expectedGroups = new List<Day24Solver.Group>
            {
                new Day24Solver.Group(Day24Solver.Group.GroupType.ImmuneSystem, 17, 5390, 4507, "fire", 2),
                new Day24Solver.Group(Day24Solver.Group.GroupType.ImmuneSystem, 989, 1274, 25, "slashing", 3),
                new Day24Solver.Group(Day24Solver.Group.GroupType.Infection, 801, 4706, 116, "bludgeoning", 1),
                new Day24Solver.Group(Day24Solver.Group.GroupType.Infection, 4485, 2961, 12, "slashing", 4)
            };
            expectedGroups[0].Weaknesses.Add("radiation");
            expectedGroups[0].Weaknesses.Add("bludgeoning");
            expectedGroups[1].Immunities.Add("fire");
            expectedGroups[1].Weaknesses.Add("bludgeoning");
            expectedGroups[1].Weaknesses.Add("slashing");
            expectedGroups[3].Immunities.Add("radiation");
            expectedGroups[3].Weaknesses.Add("fire");
            expectedGroups[3].Weaknesses.Add("cold");

            var combat = dataProdiver.GetData();
            Assert.Equal(expectedGroups, combat.Groups, new GroupComparer());
        }
        
        private class GroupComparer : IEqualityComparer<Day24Solver.Group>
        {
            public bool Equals(Day24Solver.Group x, Day24Solver.Group y)
            {
                return x.Type == y.Type &&
                       x.Units == y.Units &&
                       x.HitPoints == y.HitPoints &&
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