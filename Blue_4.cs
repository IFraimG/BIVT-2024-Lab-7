using System;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _counts;

            public string Name => _name;

            public int[] Scores => this._counts?.Clone() as int[];
            public int TotalScore
            {
                get
                {
                    if (this._counts == null) return 0;
                    int res = 0;

                    for (int i = 0; i < this._counts.Length; i++)
                    {
                        res += this._counts[i];
                    }

                    return res;
                }
            }

            public Team(string name)
            {
                this._name = name;
                this._counts = new int[0];
            }

            public void PlayMatch(int result)
            {
                Array.Resize(ref this._counts, this._counts.Length + 1);
                this._counts[this._counts.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine(this.Name, this.TotalScore);
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
        }

        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manCount;
            private int _womanCount;

            public string Name => _name;

            public ManTeam[] ManTeams => this._manTeams;
            public WomanTeam[] WomanTeams => this._womanTeams;

            public Group(string name)
            {
                this._name = name;
                this._manTeams = new ManTeam[12];
                this._womanTeams = new WomanTeam[12];

                this._manCount = 0;
                this._womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return;
                if (team is ManTeam manTeam && this._manCount < this._manTeams.Length)
                {
                    this._manTeams[this._manCount++] = manTeam;
                }
                else if (team is WomanTeam womanTeam && this._womanCount < this._womanTeams.Length)
                {
                    _womanTeams[this._womanCount++] = womanTeam;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (Team team in teams)
                {
                    Add(team);
                }
            }

            public void Sort()
            {
                SortTeam(this._manTeams, this._manCount);
                SortTeam(this._womanTeams, this._womanCount);
            }

            private void SortTeam(Team[] teams, int count)
            {
                if (teams == null) return;
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - i - 1; j++)
                    {
                        if (teams[j + 1]?.TotalScore > teams[j]?.TotalScore)
                        {
                            Team temp = teams[j + 1];
                            teams[j + 1] = teams[j];
                            teams[j] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group = new Group("Финалисты");

                MergeTeamList(group1.ManTeams, group2.ManTeams, group._manTeams, ref group._manCount, size / 2);
                MergeTeamList(group1.WomanTeams, group2.WomanTeams, group._womanTeams, ref group._womanCount, size / 2);

                return group;
            }

            public static void MergeTeamList(Team[] team1, Team[] team2, Team[] group, ref int count, int size)
            {
                int foo1 = 0;
                int foo2 = 0;
                count = 0;

                while (foo1 < size && foo2 < size)
                {
                    if (team1[foo1]?.TotalScore >= team2[foo2]?.TotalScore)
                    {
                        group[count++] = team1[foo1++];
                    }
                    else
                    {
                        group[count++] = team2[foo2++];
                    }
                }

                while (foo1 < size && count < group.Length)
                {
                    group[count++] = team1[foo1++];
                }

                while (foo2 < size && count < group.Length)
                {
                    group[count++] = team2[foo2++];
                }
            }

            public void Print()
            {
                Console.WriteLine(Name);
            }
        }
    }
}