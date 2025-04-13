using System;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _firstName;
            protected int _counter;

            public string Name => _firstName;
            public int Votes => _counter;

            public Response(string name)
            {
                this._firstName = name;
                this._counter = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int fooSumma = 0;
                foreach (var res in responses)
                {
                    if (res.Name == this._firstName)
                    {
                        fooSumma++;
                    }
                }

                return fooSumma;
            }

            public virtual void Print()
            {
                Console.WriteLine("{0}, {1}, {2}", this.Name, this.Votes);
            }
        }

        public class HumanResponse : Response
        {
            private string _lastname;
            public string Surname => _lastname;

            public HumanResponse(string name, string lastname) : base(name)
            {
                this._lastname = lastname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;

                foreach (var res in responses)
                {
                    if (res is HumanResponse humanRes && humanRes.Name == Name && humanRes.Surname == Surname) count++;
                }

                return count;
            }

            public override void Print()
            {
                base.Print();
                Console.Write(" {0}", this.Surname);


            }
        }
    }
}