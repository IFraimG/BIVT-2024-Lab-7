using System;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _lastname;
            private int _place;
            private bool isTrue;

            public string Name => _name;
            public string Surname => _lastname;
            public int Place => _place;

            public Sportsman(string name, string lastname)
            {
                this._name = name;
                this._lastname = lastname;
                this._place = 0;
                this.isTrue = true;
            }

            public void SetPlace(int place)
            {
                if (isTrue)
                {
                    this._place = place;
                    this.isTrue = false;
                }
            }

            public void Print()
            {
                Console.WriteLine("{0} {1} {2}", Name, Surname, Place);
            }
        }

        public abstract class Team
        {
            private string _name;
            protected Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => this._sportsmen;

            public int SummaryScore
            {
                get
                {
                    if (this._sportsmen == null) return 0;
                    int summa = 0;

                    foreach (var foo in this._sportsmen)
                    {
                        switch (foo.Place)
                        {
                            case 1:
                                summa += 5;
                                break;
                            case 2:
                                summa += 4;
                                break;
                            case 3:
                                summa += 3;
                                break;
                            case 4:
                                summa += 2;
                                break;
                            case 5:
                                summa += 1;
                                break;
                            default:
                                summa += 0;
                                break;
                        }

                    }

                    return summa;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (this._sportsmen == null) return 0;
                    int foo = 18;

                    for (int i = 0; i < this._count; i++)
                    {
                        if (this._sportsmen[i].Place < foo) foo = this._sportsmen[i].Place;
                    }

                    return foo;
                }
            }

            public Team(string name)
            {
                this._name = name;
                this._sportsmen = new Sportsman[6];
                this._count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (this._sportsmen == null) return;
                if (this._count < 6)
                {
                    this._sportsmen[this._count] = sportsman;
                    this._count++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (this._sportsmen == null || sportsmen == null) return;

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);
                }

            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if ((teams[j].SummaryScore < teams[j + 1].SummaryScore) ||
                            (teams[j].SummaryScore == teams[j + 1].SummaryScore &&
                             teams[j].TopPlace > teams[j + 1].TopPlace))
                        {
                            Team temp = teams[j + 1];
                            teams[j + 1] = teams[j];
                            teams[j] = temp;
                        }
                    }
                }

            }

            public void Print()
            {
                Console.WriteLine(Name);
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;

                Team maxChampion = teams[0];
                double maxChampStrength = teams[0].GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double strength = teams[i].GetTeamStrength();
                    if (strength > maxChampStrength)
                    {
                        maxChampStrength = strength;
                        maxChampion = teams[i];
                    }
                }

                return maxChampion;
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                if (this._sportsmen == null) return 0;

                double summa = 0;
                for (int i = 0; i < this._sportsmen.Length; i++)
                {
                    summa += this._sportsmen[i].Place;
                }

                if (summa == 0) return 0;

                return 100 / (summa / this._sportsmen.Length);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                if (this._sportsmen == null) return 0;

                double summa = 0;
                double parts = 1;
                for (int i = 0; i < this._sportsmen.Length; i++)
                {
                    if (this._sportsmen[i] != null)
                    {
                        summa += this._sportsmen[i].Place;
                        parts *= this._sportsmen[i].Place;
                    }
                }

                if (parts == 0) return 0;

                return 100 * this._sportsmen.Length * summa / parts;
            }
        }
    }
}