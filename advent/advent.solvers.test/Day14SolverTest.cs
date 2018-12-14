using Xunit;

namespace advent.solvers.test
{
    public class Day14SolverTest
    {
        [Fact]
        public void SetupsFirstRecipes()
        {
            var recipes = new Day14Solver.Recipes();
            Assert.Equal("37", recipes.Scores.ToString());
            Assert.Equal(0, recipes.FirstElfIndex);
            Assert.Equal(1, recipes.SecondElfIndex);
        }

        [Fact]
        public void CalculatesNextRecipes()
        {
            var recipes = new Day14Solver.Recipes();
            recipes.CalculateNextRecipes();
            Assert.Equal("3710", recipes.Scores.ToString());
        }

        [Fact]
        public void MovesElves()
        {
            var recipes = new Day14Solver.Recipes();
            recipes.CalculateNextRecipes();
            recipes.CalculateNextRecipes();
            recipes.MoveElves();
            Assert.Equal(4, recipes.FirstElfIndex);
            Assert.Equal(3, recipes.SecondElfIndex);
        }

        [Theory]
        [InlineData(5, "0124515891")]
        [InlineData(9, "5158916779")]
        [InlineData(18, "9251071085")]
        [InlineData(2018, "5941429882")]
        public void ReturnsTenRecipesAfterCount(int count, string expectedNextRecipes)
        {
            var solver = new Day14Solver(count.ToString());
            Assert.Equal(expectedNextRecipes, solver.SolveFirstPart());
        }

        [Theory]
        [InlineData("51589", "9")]
        [InlineData("01245", "5")]
        [InlineData("92510", "18")]
        [InlineData("59414", "2018")]
        public void ReturnsRecipesBeforeInput(string input, string expectedCount)
        {
            var solver = new Day14Solver(input);
            Assert.Equal(expectedCount, solver.SolveSecondPart());
        }
    }
}