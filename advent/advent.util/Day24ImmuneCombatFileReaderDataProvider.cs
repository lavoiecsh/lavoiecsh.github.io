using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day24ImmuneCombatFileReaderDataProvider : DataProvider<Day24Solver.ImmuneCombat>
    {
        private readonly string filename;

        private readonly Regex groupParsingRegex;
        private const string ImmuneSystemStart = "Immune System:";
        private const string InfectionStart = "Infection:";

        public Day24ImmuneCombatFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            groupParsingRegex = new Regex("^(\\d+) units each with (\\d+) hit points \\((.*)\\) with an attack that does (\\d+) (\\w+) damage at initiative (\\d+)$");
        }

        public Day24Solver.ImmuneCombat GetData()
        {
            var lines = File.ReadAllLines(filename);
            var combat = new Day24Solver.ImmuneCombat();
            var isImmuneSystem = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                
                if (line == ImmuneSystemStart)
                {
                    isImmuneSystem = true;
                    continue;
                }

                if (line == InfectionStart)
                {
                    isImmuneSystem = false;
                    continue;
                }

                var group = ParseGroup(line);
                if (isImmuneSystem) combat.ImmuneSystems.Add(group);
                else combat.Infections.Add(group);
            }

            return combat;
        }

        private Day24Solver.Group ParseGroup(string line)
        {
            var match = groupParsingRegex.Match(line);
            var units = int.Parse(match.Groups[1].Value);
            var hp = int.Parse(match.Groups[2].Value);
            var weaknessesAndImmunities = match.Groups[3].Value;
            var damage = int.Parse(match.Groups[4].Value);
            var damageType = match.Groups[5].Value;
            var initiative = int.Parse(match.Groups[6].Value);
            var group = new Day24Solver.Group(units, hp, damage, damageType, initiative);
            ParseWeaknessesAndImmunities(group, weaknessesAndImmunities);
            return group;
        }

        private static void ParseWeaknessesAndImmunities(Day24Solver.Group group, string line)
        {
            var split = line.Split(';');
            foreach (var s in split)
            {
                var s2 = s.Trim();
                if (s2.StartsWith("weak to "))
                {
                    var weaknesses = s2.Substring(8).Split(", ");
                    foreach (var weakness in weaknesses) group.Weaknesses.Add(weakness);
                }

                if (s2.StartsWith("immune to "))
                {
                    var immunities = s2.Substring(10).Split(", ");
                    foreach (var immunity in immunities) group.Immunities.Add(immunity);
                }
            }
        }
    }
}