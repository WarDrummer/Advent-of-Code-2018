using System.Text;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day14A : IProblem
    {
        protected readonly ParserType Parser;
        private Recipe _head, _tail, _elf1, _elf2;
        private int _recipeCount = 2;

        public Day14A(ParserType parser) { Parser = parser; }

        public Day14A() : this(new ParserType("day14.in")){ }
        
        public virtual string Solve()
        {
            var puzzleInput = int.Parse(Parser.GetData());

            Initialize();

            while (_recipeCount < puzzleInput + 10)
                Iterate();

            for (var i = 0; i < puzzleInput; i++)
                _head = _head.Next;

            return Print().Substring(0, 10);
        }

        protected void Initialize()
        {
            _head = _elf1 = new Recipe(3);
            _tail = _elf2 = new Recipe(7);
            _head.Next = _tail;
        }

        protected void Iterate()
        {
            CreateNewRecipes();
            _elf1 = AdvanceElf(_elf1);
            _elf2 = AdvanceElf(_elf2);
        }

        protected string Print()
        {
            var tmp = _head;
            var sb = new StringBuilder();
            while (tmp != null)
            {
                sb.Append(tmp.Score);
                tmp = tmp.Next;
            }
            return sb.ToString();
        }

        protected void CreateNewRecipes()
        {
            var newRecipe = (_elf1.Score + _elf2.Score).ToString();
            foreach (var c in newRecipe)
            {
                _tail.Next = new Recipe(c - '0');
                _tail = _tail.Next;
                _recipeCount++;
            }
        }

        protected Recipe AdvanceElf(Recipe elf)
        {
            var advance = elf.Score + 1;
            for (var i = 0; i < advance; i++)
                elf = elf.Next ?? _head;
            return elf;
        }
    }

    public class Recipe
    {
        public int Score { get; }
        public Recipe Next { get; set; }

        public Recipe(int score)
        {
            Score = score;
        }
    }
}
