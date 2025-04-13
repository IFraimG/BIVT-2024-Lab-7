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
            private bool _isExpelled;

            public string Name => _name;
            public string Surname => _lastname;

            public Participant(string name, string lastname)
            {
                this._name = name;
                this._lastname = lastname;
                this._minutes = new int[] { };
                this._isExpelled = true;
            }

            public int[] Penalties
            {
                get
                {
                    if (this._minutes == null) return null;
                    int[] arr = new int[this._minutes.Length];
                    for (int i = 0; i < this._minutes.Length; i++)
                    {
                        arr[i] = this._minutes[i];
                    }

                    return arr;
                }
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
                    if (this._minutes == null || this._minutes.Length == 0) return false;

                    return this._isExpelled;
                }
            }

            public virtual void PlayMatch(int time)
            {
                if (this._minutes == null) return;
                if (time == 10) this._isExpelled = false;

                int[] arr = new int[this._minutes.Length + 1];
                for (int i = 0; i < this._minutes.Length; i++)
                {
                    arr[i] = this._minutes[i];
                }

                this._minutes = arr;
                arr[this._minutes.Length - 1] = time;
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

                    return this.Total > 0.1 * ((double)countMinutes / _players.Length);
                }
            }
        }
    }
}