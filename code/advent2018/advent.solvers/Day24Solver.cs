using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day24Solver : Solver
    {
        public string ProblemName => "Immune System Simulator 20XX";

        private readonly DataProvider<ImmuneCombat> dataProvider;

        public Day24Solver(DataProvider<ImmuneCombat> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var combat = dataProvider.GetData();
            return combat.GetWinner().Units.ToString();
        }

        public string SolveSecondPart()
        {
            var combat = dataProvider.GetData();
            var boost = GetSmallestWinningBoost(combat);
            return combat.AddBoost(boost).GetWinner().Units.ToString();
        }

        public static int GetSmallestWinningBoost(ImmuneCombat combat)
        {
            return Enumerable.Range(1, 100)
                .First(b => combat.Clone().AddBoost(b).GetWinner().Winner == Group.GroupType.ImmuneSystem);
        }

        public class ImmuneCombat
        {
            public IList<Group> Groups;

            public ImmuneCombat()
            {
                Groups = new List<Group>();
            }

            public (Group.GroupType? Winner, long Units) GetWinner()
            {
                var died = true;
                while (Groups.Any(g => g.Type == Group.GroupType.ImmuneSystem) &&
                       Groups.Any(g => g.Type == Group.GroupType.Infection) &&
                       died)
                    died = Fight();
                var winner = Groups.All(g => g.Type == Group.GroupType.ImmuneSystem) ? Group.GroupType.ImmuneSystem :
                    Groups.All(g => g.Type == Group.GroupType.Infection) ? Group.GroupType.Infection :
                    (Group.GroupType?) null;
                return (winner, Groups.Sum(g => g.Units));
            }

            public ImmuneCombat AddBoost(int boost)
            {
                foreach (var group in Groups.Where(g => g.Type == Group.GroupType.ImmuneSystem)) group.AddBoost(boost);
                return this;
            }

            public bool Fight()
            {
                var unitsBefore = Groups.ToDictionary(g => g, g => g.Units);
                var targets = SelectTargets().ToList().OrderByDescending(t => t.Attacker.Initiative);
                foreach (var (attacker, defender) in targets) attacker.Attack(defender);

                Groups = Groups.Where(g => g.Units > 0).ToList();
                return unitsBefore.Any(kv => kv.Key.Units != kv.Value);
            }

            private IEnumerable<(Group Attacker, Group Defender)> SelectTargets()
            {
                var remainingGroups = Groups.ToList();
                return Groups
                    .OrderByDescending(g => (g.EffectivePower(), g.Initiative))
                    .Select(g => (g, g.ChooseTarget(remainingGroups)));
            }

            public ImmuneCombat Clone()
            {
                return new ImmuneCombat {Groups = Groups.Select(g => g.Clone()).ToList()};
            }
        }

        public class Group
        {
            public readonly GroupType Type;
            public long Units { get; private set; }
            public readonly int HitPoints;
            public int Damage { get; private set; }
            public readonly int Initiative;
            public readonly string DamageType;
            public IList<string> Weaknesses { get; private set; }
            public IList<string> Immunities { get; private set; }

            public Group(GroupType type, long units, int hitPoints, int damage, string damageType, int initiative)
            {
                Type = type;
                Units = units;
                HitPoints = hitPoints;
                Damage = damage;
                DamageType = damageType;
                Initiative = initiative;
                Weaknesses = new List<string>();
                Immunities = new List<string>();
            }

            public Group Clone()
            {
                return new Group(Type, Units, HitPoints, Damage, DamageType, Initiative)
                {
                    Weaknesses = new List<string>(Weaknesses),
                    Immunities = new List<string>(Immunities)
                };
            }

            public void AddBoost(int boost)
            {
                Damage += boost;
            }

            private long DamageDealtTo(Group defender)
            {
                if (defender.Immunities.Contains(DamageType)) return 0;
                var power = EffectivePower();
                if (defender.Weaknesses.Contains(DamageType)) power *= 2;
                return power;
            }

            public void Attack(Group defender)
            {
                if (Units <= 0) return;
                if (defender == null) return;
                defender.Units -= DamageDealtTo(defender) / defender.HitPoints;
            }

            public Group ChooseTarget(IList<Group> remainingGroups)
            {
                var target = remainingGroups.Where(g => g.Type != Type)
                    .Select(g => (g, DamageDealtTo(g)))
                    .Where(gd => gd.Item2 != 0)
                    .OrderByDescending(gd => (gd.Item2, gd.g.EffectivePower(), gd.g.Initiative))
                    .FirstOrDefault()
                    .g;
                remainingGroups.Remove(target);
                return target;
            }

            public long EffectivePower() => Units * Damage;

            public override string ToString()
            {
                return $"{Type} {Units}";
            }

            public enum GroupType
            {
                ImmuneSystem,
                Infection
            }
        }
    }
}