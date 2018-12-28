using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day15SolverTest
    {
        private readonly Day15Solver.Map map;
        private readonly Day15Solver.Map sample1;
        private readonly IList<Day15Solver.Map> maps;
        private readonly IList<IList<Day15Solver.Unit>> expectedEndGames;
        private readonly IList<IList<Day15Solver.Unit>> expectedMinimumWinningEndGames;

        private readonly Mock<DataProvider<Day15Solver.Map>> dataProvider;
        private readonly Solver solver;

        public Day15SolverTest()
        {
            var openTiles = new List<Day15Solver.Position>
            {
                (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                (1, 2), (2, 2), (3, 2), (4, 2), (5, 2),
                (1, 3), (3, 3), (5, 3),
                (1, 4), (2, 4), (3, 4), (5, 4),
                (1, 5), (2, 5), (3, 5), (4, 5), (5, 5)
            };
            var units = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf),
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((3, 4), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf)
            };
            map = new Day15Solver.Map(openTiles, units);

            sample1 = new Day15Solver.Map(new List<Day15Solver.Position>
                {
                    (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                    (1, 2), (2, 2), (3, 2), (5, 2),
                    (1, 3), (2, 3), (3, 3), (5, 3)
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Elf),
                    new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((2, 3), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin)
                });

            maps = new List<Day15Solver.Map>
            {
                map,
                new Day15Solver.Map(new List<Day15Solver.Position>
                    {
                        (1, 1), (2, 1), (3, 1), (5, 1),
                        (1, 2), (3, 2), (4, 2), (5, 2),
                        (1, 3), (2, 3), (5, 3),
                        (1, 4), (2, 4), (3, 4), (5, 4),
                        (1, 5), (2, 5), (3, 5), (4, 5), (5, 5)
                    },
                    new List<Day15Solver.Unit>
                    {
                        new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 1), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((1, 2), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((1, 3), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((4, 5), Day15Solver.Unit.UnitType.Elf)
                    }),
                new Day15Solver.Map(new List<Day15Solver.Position>
                    {
                        (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                        (1, 2), (3, 2), (4, 2), (5, 2),
                        (1, 3), (2, 3), (5, 3),
                        (1, 4), (2, 4), (3, 4), (5, 4),
                        (1, 5), (2, 5), (3, 5), (5, 5)
                    },
                    new List<Day15Solver.Unit>
                    {
                        new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((5, 1), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((1, 3), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((1, 4), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((3, 5), Day15Solver.Unit.UnitType.Elf)
                    }),
                new Day15Solver.Map(new List<Day15Solver.Position>
                    {
                        (1, 1), (2, 1), (3, 1), (5, 1),
                        (1, 2), (3, 2), (4, 2), (5, 2),
                        (1, 3), (2, 3), (4, 3), (5, 3),
                        (1, 4), (2, 4), (3, 4), (5, 4),
                        (1, 5), (2, 5), (3, 5), (4, 5), (5, 5)
                    },
                    new List<Day15Solver.Unit>
                    {
                        new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((3, 1), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((1, 3), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((1, 4), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((4, 5), Day15Solver.Unit.UnitType.Elf)
                    }),
                new Day15Solver.Map(new List<Day15Solver.Position>
                    {
                        (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                        (1, 2), (3, 2), (4, 2), (5, 2),
                        (1, 3), (5, 3),
                        (1, 4), (3, 4), (5, 4),
                        (1, 5), (2, 5), (3, 5), (5, 5)
                    },
                    new List<Day15Solver.Unit>
                    {
                        new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((1, 4), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((3, 4), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((5, 5), Day15Solver.Unit.UnitType.Goblin)
                    }),
                new Day15Solver.Map(new List<Day15Solver.Position>
                    {
                        (1, 1), (2, 1), (3, 1), (4, 1), (5, 1), (6, 1), (7, 1),
                        (1, 2), (2, 2), (3, 2), (5, 2), (6, 2), (7, 2),
                        (1, 3), (2, 3), (5, 3), (6, 3), (7, 3),
                        (1, 4), (2, 4), (3, 4), (6, 4), (7, 4),
                        (1, 5), (2, 5), (3, 5), (5, 5), (6, 5), (7, 5),
                        (1, 6), (2, 6), (3, 6), (4, 6), (5, 6), (6, 6), (7, 6),
                        (1, 7), (2, 7), (3, 7), (4, 7), (5, 7), (6, 7), (7, 7)
                    },
                    new List<Day15Solver.Unit>
                    {
                        new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((2, 2), Day15Solver.Unit.UnitType.Elf),
                        new Day15Solver.Unit((7, 3), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((2, 6), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((6, 6), Day15Solver.Unit.UnitType.Goblin),
                        new Day15Solver.Unit((6, 7), Day15Solver.Unit.UnitType.Goblin)
                    })
            };

            expectedEndGames = new List<IList<Day15Solver.Unit>>
            {
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                    new Day15Solver.Unit((2, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 131},
                    new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 59},
                    new Day15Solver.Unit((5, 5), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200}
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((5, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 200},
                    new Day15Solver.Unit((1, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 197},
                    new Day15Solver.Unit((2, 3), Day15Solver.Unit.UnitType.Elf) {HitPoints = 185},
                    new Day15Solver.Unit((1, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 200},
                    new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 200}
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 164},
                    new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 197},
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Elf),
                    new Day15Solver.Unit((1, 3), Day15Solver.Unit.UnitType.Elf) {HitPoints = 98},
                    new Day15Solver.Unit((2, 4), Day15Solver.Unit.UnitType.Elf)
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((3, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 98},
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 95},
                    new Day15Solver.Unit((4, 5), Day15Solver.Unit.UnitType.Goblin)
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((1, 5), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 98},
                    new Day15Solver.Unit((3, 5), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 38},
                    new Day15Solver.Unit((5, 5), Day15Solver.Unit.UnitType.Goblin)
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 137},
                    new Day15Solver.Unit((1, 2), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((2, 3), Day15Solver.Unit.UnitType.Goblin),
                    new Day15Solver.Unit((2, 5), Day15Solver.Unit.UnitType.Goblin)
                }
            };

            expectedMinimumWinningEndGames = new List<IList<Day15Solver.Unit>>
            {
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((3, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 158},
                    new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 14}
                },
                new List<Day15Solver.Unit>(),
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Elf),
                    new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 23},
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Elf),
                    new Day15Solver.Unit((1, 3), Day15Solver.Unit.UnitType.Elf) {HitPoints = 125},
                    new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Elf),
                    new Day15Solver.Unit((2, 4), Day15Solver.Unit.UnitType.Elf)
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 8},
                    new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 86}
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Elf) {HitPoints = 14},
                    new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 152}
                },
                new List<Day15Solver.Unit>
                {
                    new Day15Solver.Unit((2, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 38}
                }
            };

            dataProvider = new Mock<DataProvider<Day15Solver.Map>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(map);

            solver = new Day15Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsPositionsInRange()
        {
            var inRange = sample1.Units[0].PositionsInRange(sample1);
            var expectedInRange = new List<Day15Solver.Position>
            {
                (3, 1), (5, 1),
                (2, 2), (5, 2),
                (1, 3), (3, 3)
            };
            Assert.Equal(expectedInRange.OrderByReading(), inRange.OrderByReading());
        }

        [Fact]
        public void ReturnsReachablePositions()
        {
            var reachable = sample1.Units[0].ReachablePositions(sample1);
            var expectedReachable = new List<Day15Solver.Position>
            {
                (3, 1),
                (2, 2),
                (1, 3), (3, 3)
            };
            Assert.Equal(expectedReachable.OrderByReading(), reachable.OrderByReading());
        }

        [Fact]
        public void ReturnsNearestPosition()
        {
            var nearest = sample1.Units[0].NearestPosition(sample1);
            var expectedNearest = (3, 1);
            Assert.Equal(expectedNearest, nearest);
        }

        [Fact]
        public void MovesTowardsNearestPosition()
        {
            sample1.Units[0].Move(sample1);
            Assert.Equal((2, 1), sample1.Units[0].Position);
        }

        [Fact]
        public void AttacksIfAUnitIsInRange()
        {
            map.Units[1].Play(map);
            Assert.Equal(197, map.Units[2].HitPoints);
        }

        [Fact]
        public void PlaysFirstRound()
        {
            map.PlayRound();
            var expectedUnits = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((3, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 197},
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 197},
                new Day15Solver.Unit((3, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 197},
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 197}
            };
            Assert.Equal(expectedUnits.OrderByReading(), map.Units.OrderByReading(), new UnitComparer());
        }

        [Fact]
        public void PlaysSecondRound()
        {
            for (var i = 0; i < 2; ++i) map.PlayRound();
            var expectedUnits = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf) {HitPoints = 188},
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 194},
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 194},
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 194}
            };
            Assert.Equal(expectedUnits.OrderByReading(), map.Units.OrderByReading(), new UnitComparer());
        }

        [Fact]
        public void Plays23Rounds()
        {
            for (var i = 0; i < 23; ++i) map.PlayRound();
            var expectedUnits = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((3, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 131},
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 131},
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 131}
            };
            Assert.Equal(expectedUnits.OrderByReading(), map.Units.OrderByReading(), new UnitComparer());
        }

        [Fact]
        public void Plays24Rounds()
        {
            for (var i = 0; i < 24; ++i) map.PlayRound();
            var expectedUnits = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((3, 1), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 131},
                new Day15Solver.Unit((3, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 200},
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin) {HitPoints = 128},
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf) {HitPoints = 128}
            };
            Assert.Equal(expectedUnits.OrderByReading(), map.Units.OrderByReading(), new UnitComparer());
        }

        [Theory]
        [InlineData(0, 47)]
        [InlineData(1, 37)]
        [InlineData(2, 46)]
        [InlineData(3, 35)]
        [InlineData(4, 54)]
        [InlineData(5, 20)]
        public void PlaysFullGame(int mapNumber, int expectedRounds)
        {
            var rounds = maps[mapNumber].Play();
            Assert.Equal(expectedRounds, rounds);
            Assert.Equal(expectedEndGames[mapNumber].OrderByReading(),
                maps[mapNumber].Units.OrderByReading(),
                new UnitComparer());
        }

        [Theory]
        [InlineData(0, "27730")]
        [InlineData(1, "36334")]
        [InlineData(2, "39514")]
        [InlineData(3, "27755")]
        [InlineData(4, "28944")]
        [InlineData(5, "18740")]
        public void ReturnsOutcomeOfGame(int mapNumber, string expectedOutcome)
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(maps[mapNumber]);
            Assert.Equal(expectedOutcome, solver.SolveFirstPart());
        }

        [Theory]
        [InlineData(0, 15, 29)]
        [InlineData(2, 4, 33)]
        [InlineData(3, 15, 37)]
        [InlineData(4, 12, 39)]
        [InlineData(5, 34, 30)]
        public void ReturnsMinimumAttackToWin(int mapNumber, int expectedAttack, int expectedRounds)
        {
            var (attack, rounds) = maps[mapNumber].FindWinningAttack();
            Assert.Equal(expectedAttack, attack);
            Assert.Equal(expectedRounds, rounds);
            Assert.Equal(expectedMinimumWinningEndGames[mapNumber].OrderByReading(),
                maps[mapNumber].Units.OrderByReading(),
                new UnitComparer());
        }

        [Theory]
        [InlineData(0, "4988")]
        [InlineData(2, "31284")]
        [InlineData(3, "3478")]
        [InlineData(4, "6474")]
        [InlineData(5, "1140")]
        public void ReturnsOutcomeOfMinimumWinningGame(int mapNumber, string expectedOutcome)
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(maps[mapNumber]);
            Assert.Equal(expectedOutcome, solver.SolveSecondPart());
        }
    }

    public class UnitComparer : IEqualityComparer<Day15Solver.Unit>
    {
        public bool Equals(Day15Solver.Unit x, Day15Solver.Unit y)
        {
            return Equals(x.Position, y.Position) && x.Type == y.Type && x.HitPoints == y.HitPoints;
        }

        public int GetHashCode(Day15Solver.Unit obj)
        {
            throw new System.NotImplementedException();
        }
    }
}