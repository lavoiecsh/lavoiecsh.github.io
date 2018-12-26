using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day24ImmuneCombatFileReaderDataProvider : DataProvider<Day24Solver.ImmuneCombat>
    {
        private readonly string filename;

        private readonly Regex groupWithWeaknessImmunityParsingRegex;
        private readonly Regex groupWithoutWeaknessImmunityParsinRegex;
        private const string ImmuneSystemStart = "Immune System:";
        private const string InfectionStart = "Infection:";

        public Day24ImmuneCombatFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            groupWithWeaknessImmunityParsingRegex =
                new Regex(
                    "^(\\d+) units each with (\\d+) hit points \\((.*)\\) with an attack that does (\\d+) (\\w+) damage at initiative (\\d+)$");
            groupWithoutWeaknessImmunityParsinRegex = new Regex(
                "^(\\d+) units each with (\\d+) hit points with an attack that does (\\d+) (\\w+) damage at initiative (\\d+)$");
        }

        public Day24Solver.ImmuneCombat GetData()
        {
            var lines = File.ReadAllLines(filename);
            var combat = new Day24Solver.ImmuneCombat();
            var groupType = Day24Solver.Group.GroupType.ImmuneSystem;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line == ImmuneSystemStart)
                {
                    groupType = Day24Solver.Group.GroupType.ImmuneSystem;
                    continue;
                }

                if (line == InfectionStart)
                {
                    groupType = Day24Solver.Group.GroupType.Infection;
                    continue;
                }

                combat.Groups.Add(ParseGroup(line, groupType));
            }

            return combat;
        }

        private Day24Solver.Group ParseGroup(string line, Day24Solver.Group.GroupType groupType)
        {
            var match = groupWithWeaknessImmunityParsingRegex.Match(line);
            if (match.Success) return ParseGroupWithWeaknessImmunities(match, groupType);

            match = groupWithoutWeaknessImmunityParsinRegex.Match(line);
            return ParseGroupWithoutWeaknessImmunities(match, groupType);
        }

        private static Day24Solver.Group ParseGroupWithWeaknessImmunities(Match match, Day24Solver.Group.GroupType type)
        {
            var units = int.Parse(match.Groups[1].Value);
            var hitPoints = int.Parse(match.Groups[2].Value);
            var weaknessesAndImmunities = match.Groups[3].Value;
            var damage = int.Parse(match.Groups[4].Value);
            var damageType = match.Groups[5].Value;
            var initiative = int.Parse(match.Groups[6].Value);
            var group = new Day24Solver.Group(type, units, hitPoints, damage, damageType, initiative);
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

        private static Day24Solver.Group ParseGroupWithoutWeaknessImmunities(Match match,
            Day24Solver.Group.GroupType type)
        {
            var units = int.Parse(match.Groups[1].Value);
            var hitPoints = int.Parse(match.Groups[2].Value);
            var damage = int.Parse(match.Groups[3].Value);
            var damageType = match.Groups[4].Value;
            var initiative = int.Parse(match.Groups[5].Value);
            return new Day24Solver.Group(type, units, hitPoints, damage, damageType, initiative);
        }
    }
}