using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }

        public string SolveSecondPart()
        {
            throw new System.NotImplementedException();
        }

        public class ImmuneCombat
        {
            public readonly IList<Group> ImmuneSystems;
            public readonly IList<Group> Infections;

            public ImmuneCombat()
            {
                ImmuneSystems = new List<Group>();
                Infections = new List<Group>();
            }
        }

        public class Group
        {
            public int Units { get; private set; }
            public readonly int HP;
            public readonly int Damage;
            public readonly int Initiative;
            public readonly string DamageType;
            public readonly IList<string> Weaknesses;
            public readonly IList<string> Immunities;

            public Group(int units, int hp, int damage, string damageType, int initiative)
            {
                Units = units;
                HP = hp;
                Damage = damage;
                DamageType = damageType;
                Initiative = initiative;
                Weaknesses = new List<string>();
                Immunities = new List<string>();
            }
        }
    }
}