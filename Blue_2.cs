using System;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _lastname;
            private int[,] _marks;
            private int _count;

            public readonly string Name => _name;
            public readonly string Surname => _lastname;

            public readonly int[,] Marks
            {
                get
                {
                    if (this._marks == null) return null;

                    int[,] arr = new int[this._marks.GetLength(0), this._marks.GetLength(1)];

                    for (int i = 0; i < this._marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < this._marks.GetLength(1); j++)
                        {
                            arr[i, j] = this._marks[i, j];
                        }
                    }

                    return arr;

                }
            }

            public Participant(string name, string lastname)
            {
                this._name = name;
                this._lastname = lastname;
                this._marks = new int[2, 5];
                this._count = 0;
            }

            public int TotalScore
            {
                get
                {
                    if (this._marks == null || (this._marks.GetLength(0) == 0 && this._marks.GetLength(1) == 0))
                        return 0;

                    int summa = 0;
                    for (int i = 0; i < this._marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < this._marks.GetLength(1); j++)
                        {
                            summa += this._marks[i, j];
                        }
                    }

                    return summa;
                }
            }

            public void Jump(int[] result)
            {
                if (result == null || this._marks == null || this._count > 1) return;

                for (int j = 0; j < this._marks.GetLength(1); j++)
                {
                    if (this._count == 0) this._marks[0, j] = result[j];
                    else if (this._count == 1) this._marks[1, j] = result[j];
                }

                this._count++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }

            }

            public void Print()
            {
                Console.WriteLine("{0} {1} {2}", this._name, this._lastname, this.TotalScore);
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private int _count;
            private Participant[] _parts;

            public string Name => _name;
            public int Bank => _bank;
            public int Count => _count;

            public Participant[] Participants
            {
                get
                {
                    if (this._parts == null) return null;

                    return this._parts.Take(this._count).ToArray();
                }
            }

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                this._name = name;
                this._bank = bank;
                this._parts = new Participant[] { };
                this._count = 0;
            }

            public void Add(Participant participant)
            {
                if (this._parts == null) return;
                Array.Resize(ref this._parts, this._parts.Length + 1);
                this._parts[this._parts.Length - 1] = participant;
                this._count++;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                foreach (var participant in participants)
                {
                    this.Add(participant);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank)
            {
            }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    return new double[] { 0.5 * Bank, 0.3 * Bank, 0.2 * Bank };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank)
            {
            }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    int countLimit = Participants.Length / 2;
                    if (countLimit < 3) return null;
                    if (countLimit > 10) countLimit = 10;

                    double[] prizeList = new double[countLimit];
                    double result = (0.2 * Bank) / countLimit;

                    for (int i = 0; i < countLimit; i++)
                    {
                        if (i == 0) prizeList[0] = 0.4 * Bank + result;
                        if (i == 1) prizeList[1] = 0.25 * Bank + result;
                        if (i == 2) prizeList[2] = 0.15 * Bank + result;
                        else prizeList[i] = result;
                    }

                    return prizeList;
                }
            }
        }
    }
}