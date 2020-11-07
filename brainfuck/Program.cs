using System;
using System.IO;
using System.Linq;

namespace brainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                var filePath = args[0];
                if (File.Exists(filePath))
                {
                    var f = File.OpenText(filePath).ReadToEnd();
                    new Brainfuck().Read(f);
                }
            }
        }
    }

    internal class Cell
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

    internal class Brainfuck
    {
        private readonly Cell[] _cells = Enumerable.Range(1, 300000).Select(_ => new Cell()).ToArray();
        private int _current;

        public void Read(string bf)
        {
            var loopDepth = 0;
            var loopCode = "";
            for (var i = 0; i < bf.Length; i++)
            {
                if (loopDepth > 0)
                {
                    if (bf[i] == ']')
                    {
                        loopDepth--;
                        Loop(loopCode);
                        loopCode = "";
                    }
                    else
                    {
                        loopCode += bf[i];
                    }
                }
                else
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
                            loopDepth++;
                            break;
                    }
                }
            }
        }

        private void IncrementCurrent()
        {
            try
            {
                _cells[_current].Increment();
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(_current);
            }
        }

        private void DecrementCurrent()
        {
            _cells[_current].Decrement();
        }

        private void Increment()
        {
            _current++;
        }

        private void Decrement()
        {
            _current--;
        }

        private void Print()
        {
            _cells[_current].Print();
        }

        private void Input()
        {
            _cells[_current].Set(Convert.ToByte(Console.Read()));
        }

        private void Loop(string bf)
        {
            while (_cells[_current].Value != 0)
            {
                Read(bf);
            }
        }
    }
}