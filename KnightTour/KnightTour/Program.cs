using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightTour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input n for the size of chessboard(Maximum 8): ");
            int n = 0;
            while (true)
            {
                n = int.Parse(Console.ReadLine());
                if (n > 0 && n < 9)
                {
                    break;
                }    
                Console.Write("Invalid input. Please try again: ");
            }

            Console.WriteLine("Choose a starting point: ");
            Console.Write("X: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Y: ");
            int y = int.Parse(Console.ReadLine());

            KnightTour knightTour = new KnightTour(n, x, y);
            knightTour.Solve();
            Console.ReadLine();
        }
    }

    public class KnightTour
    {
        private int _boardSize;
        private int _xStart;
        private int _yStart;
        private int[,] _solutionMatrix;
        private int[] _xMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
        private int[] _yMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };

        public KnightTour(int boardSize, int xStart, int yStart)
        {
            _boardSize = boardSize;
            _solutionMatrix = new int[_boardSize, _boardSize];
            InitializeBoard();
            _xStart = xStart;
            _yStart = yStart;
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < _boardSize; i++)
                for (int j = 0; j < _boardSize; j++)
                    _solutionMatrix[i, j] = -1;
        }

        public bool Solve()
        {
            _solutionMatrix[_xStart, _yStart] = 0;
            if (!SolveProblem(1, _xStart, _yStart))
            {
                Console.WriteLine("No feasible solution found.");
                return false;
            }
            else
            {
                PrintSolution();
                return true;
            }
        }

        private bool SolveProblem(int stepCount, int x, int y)
        {
            if (stepCount == _boardSize * _boardSize)
                return true;

            for (int i = 0; i < _xMoves.Length; i++)
            {
                int newX = x + _xMoves[i];
                int newY = y + _yMoves[i];

                if (IsValidMove(newX, newY))
                {
                    _solutionMatrix[newX, newY] = stepCount;

                    if (SolveProblem(stepCount + 1, newX, newY))
                        return true;

                    // Backtracking
                    _solutionMatrix[newX, newY] = -1;
                }
            }

            return false;
        }

        private bool IsValidMove(int x, int y)
        {
            if (x < 0 || x >= _boardSize || y < 0 || y >= _boardSize)
                return false;
            if (_solutionMatrix[x, y] != -1)
                return false;
            return true;
        }

        private void PrintSolution()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                    Console.Write(" " + _solutionMatrix[i, j]);
                Console.WriteLine();
            }
        }
    }

}
