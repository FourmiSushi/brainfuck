using System;
using System.Linq;

namespace brainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = Convert.ToChar(Console.Read());
            Console.WriteLine(a);
        }
    }

    class Cell
    {
        public byte Value;

        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }

        public void Print()
        {
            Console.Write(Convert.ToChar(Value));
        }

        public void Set(byte v)
        {
            Value = v;
        }
    }

    class Brainfuck
    {
        private Cell[] Cells = Enumerable.Repeat(new Cell(), 30000).ToArray();

        private int _current = 0;

        public void Read(string bf)
        {
            var loop = false;
            var loopBf = "";
            for (var i = 0; i < bf.Length; i++)
            {
                switch (bf[i])
                {
                    case '+':
                        IncrementCurrent();
                        break;
                    case '-':
                        DecrementCurrent();
                        break;
                    case '>':
                        Increment();
                        break;
                    case '<':
                        Decrement();
                        break;
                    case '.':
                        Print();
                        break;
                    case ',':
                        Input();
                        break;
                    case '[':
                        loop = true;
                        break;
                    case ']':
                        loop = false;
                        
                        break;
                }
            }
        }

        public void IncrementCurrent()
        {
            Cells[_current].Increment();
        }

        public void DecrementCurrent()
        {
            Cells[_current].Decrement();
        }

        public void Increment()
        {
            _current++;
        }

        public void Decrement()
        {
            _current--;
        }

        public void Print()
        {
            Cells[_current].Print();
        }

        public void Input()
        {
            Cells[_current].Set(Convert.ToByte(Console.Read()));
        }

        public void Loop(string bf)
        { 
            while (Cells[_current].Value != 0)
            {
                Read(bf);
            }
        }
    }
}