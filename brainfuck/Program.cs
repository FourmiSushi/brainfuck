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
        private readonly Cell[] _cells = Enumerable.Range(1, 30000).Select(_ => new Cell()).ToArray();
        private int _current;

        public void Read(string bf)
        {
            var loopDepth = 0;
            var loopCode = "";
            foreach (var t in bf)
            {
                if (loopDepth > 0)
                {
                    if (t == ']')
                    {
                        loopDepth--;
                        if (loopDepth == 0)
                        {
                            Loop(loopCode);
                            loopCode = "";
                            continue;
                        }
                    }

                    if (t == '[')
                    {
                        loopDepth++;
                    }

                    loopCode += t;
                }
                else
                {
                    switch (t)
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
            _cells[_current].Increment();
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