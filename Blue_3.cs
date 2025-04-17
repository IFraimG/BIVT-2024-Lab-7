using System;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _lastname;
            protected int[] _minutes;

            public string Name => _name;
            public string Surname => _lastname;
            public int[] Penalties => _minutes?.Clone() as int[];

            public Participant(string name, string lastname)
            {
                this._name = name;
                this._lastname = lastname;
                this._minutes = new int[] { };
            }


            public int Total
            {
                get
                {
                    if (this._minutes == null) return 0;
                    int res = 0;
                    for (int i = 0; i < this._minutes.Length; i++)
                    {
                        res += this._minutes[i];
                    }

                    return res;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (this._minutes == null) return false;
                    foreach (int item in this._minutes)
                    {
                        if (item == 10) return true;
                    }
                    return false;
                }
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0) return;

                Array.Resize(ref this._minutes, this._minutes.Length + 1);
                this._minutes[this._minutes.Length - 1] = time;
            }

            public static void Sort(Participant[] arr)
            {
                if (arr == null) return;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = 0; j < arr.Length - i - 1; j++)
                    {
                        if (arr[j + 1].Total < arr[j].Total)
                        {
                            Participant temp = arr[j + 1];
                            arr[j + 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("{0} {1} {2}", this.Name, this.Surname, this.Total);
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
            }

            public override bool IsExpelled
            {
                get
                {
                    if (this._minutes == null || this._minutes.Length == 0) return false;

                    int foulsCount = 0;
                    foreach (int fouls in this._minutes)
                    {
                        if (fouls == 5) foulsCount++;
                    }

                    return (double)foulsCount / this._minutes.Length > 0.1 || this.Total > 2 * this._minutes.Length;
                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls >= 0 && fouls <= 5) base.PlayMatch(fouls);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static Participant[] _players = new Participant[] { };

            public HockeyPlayer(string name, string lastname) : base(name, lastname)
            {
                Array.Resize(ref _players, _players.Length + 1);
                _players[_players.Length - 1] = this;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (base.IsExpelled) return true;

                    if (_players.Length == 0) return false;

                    int countMinutes = 0;

                    foreach (var player in _players)
                    {
                        countMinutes += player.Total;
                    }

                    double average = (double) countMinutes / _players.Length;
                    return Total > 0.1 * average;
                }
            }
        }
    }
}