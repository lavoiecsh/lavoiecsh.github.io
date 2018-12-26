using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day24SolverTest
    {
        private readonly Day24Solver.ImmuneCombat sample;

        private readonly Day24Solver solver;

        public Day24SolverTest()
        {
            var groups = new List<Day24Solver.Group>
            {
                new Day24Solver.Group(Day24Solver.Group.GroupType.ImmuneSystem, 17, 5390, 4507, "fire", 2),
                new Day24Solver.Group(Day24Solver.Group.GroupType.ImmuneSystem, 989, 1274, 25, "slashing", 3),
                new Day24Solver.Group(Day24Solver.Group.GroupType.Infection, 801, 4706, 116, "bludgeoning", 1),
                new Day24Solver.Group(Day24Solver.Group.GroupType.Infection, 4485, 2961, 12, "slashing", 4)
            };
            groups[0].Weaknesses.Add("radiation");
            groups[0].Weaknesses.Add("bludgeoning");
            groups[1].Immunities.Add("fire");
            groups[1].Weaknesses.Add("bludgeoning");
            groups[1].Weaknesses.Add("slashing");
            groups[2].Weaknesses.Add("radiation");
            groups[3].Immunities.Add("radiation");
            groups[3].Weaknesses.Add("fire");
            groups[3].Weaknesses.Add("cold");
            sample = new Day24Solver.ImmuneCombat();
            sample.Groups.Add(groups[0]);
            sample.Groups.Add(groups[1]);
            sample.Groups.Add(groups[2]);
            sample.Groups.Add(groups[3]);

            var dataProvider = new Mock<DataProvider<Day24Solver.ImmuneCombat>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(sample);
            solver = new Day24Solver(dataProvider.Object);
        }

        [Fact]
        public void ExecutesFight()
        {
            sample.Fight();
            Assert.Equal(1, sample.Groups.Count(g => g.Type == Day24Solver.Group.GroupType.ImmuneSystem));
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.ImmuneSystem && g.Units == 905));
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.Infection && g.Units == 797));
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.Infection && g.Units == 4434));
        }

        [Fact]
        public void ExecutesSecondFight()
        {
            sample.Fight();
            sample.Fight();
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.ImmuneSystem && g.Units == 761));
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.Infection && g.Units == 793));
            Assert.True(sample.Groups.Any(g => g.Type == Day24Solver.Group.GroupType.Infection && g.Units == 4434));
        }

        [Fact]
        public void ReturnsUnitCountOfWinningArmy()
        {
            Assert.Equal("5216", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsSmallestWinningBoost()
        {
            Assert.Equal(1570, Day24Solver.GetSmallestWinningBoost(sample));
        }

        [Fact]
        public void ReturnsUnitsCountsOfWinningArmyWithSmallestBoost()
        {
            Assert.Equal("51", solver.SolveSecondPart());
        }
    }
}