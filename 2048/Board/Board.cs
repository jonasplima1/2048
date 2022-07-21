using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2048.Board
{
    public class Board
    {
        public void StartGame()
        {
            Console.Clear();
            int[,] board = new int[4, 4];
            GenerateNewBlock(ref board);
            GenerateNewBlock(ref board);
            PrintBoard(board);
        WAITUSER:
            Console.Clear();
            PrintBoard(board);
            //Wait for player input
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    GoDown(ref board);
                    GenerateNewBlock(ref board);
                    goto WAITUSER;
                case ConsoleKey.UpArrow:
                    GoUp(ref board);
                    GenerateNewBlock(ref board);
                    goto WAITUSER;
                case ConsoleKey.LeftArrow:
                    GoLeft();
                    goto WAITUSER;
                case ConsoleKey.RightArrow:
                    GoRight();
                    goto WAITUSER;
                case ConsoleKey.Home:
                    return;
                default:
                    goto WAITUSER;
            }
        }
        private void PrintBoard(int[,] matrix)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        //Given a direction, combine the numbers on matrix
        private void GoDown(ref int[,] board)
        {
            int[] stop = { 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                var temp = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                    {
                        continue;
                    }
                    if (board[i + 1, j] == 0)
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i + 1, j] = temp;
                    }
                    if (board[i + 1, j] == board[i, j])
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i + 1, j] += temp;
                    }
                }
            }
        }
        private void GoUp(ref int[,] board)
        {
            int[] stop = { 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                var temp = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                    {
                        continue;
                    }
                    if (board[i - 1, j] == 0)
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i - 1, j] = temp;
                    }
                    if (board[i - 1, j] == board[i, j])
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i - 1, j] += temp;
                    }
                }
            }
        }
        private void GoLeft()
        {

        }
        private void GoRight()
        {

        }


        //Given a matrix, generate a new number in empty block | 90% chance to spawn 2 and 10% 4
        private int[,] GenerateNewBlock(ref int[,] matrix)
        {
            //Generate list of empty positions
            var emptyBlocks = GetEmptyBlocks(matrix);
            if (emptyBlocks.Count == 0)
            {
                return matrix;
            }
            //Select empty coordinate
            var coordinate = SelectPosition(emptyBlocks);
            //Generate new number on that position
            matrix[coordinate[0], coordinate[1]] = GetNumber();
            //Return updated matrix;
            return matrix;
            //Generate number using the chances given
            int GetNumber()
            {
                Random random = new Random();
                const double margin = 90.0 / 100.0;
                return random.NextDouble() <= margin ? 2 : 4;
            }
        }

        //Generate a random coordinate for a 4x4 matrix
        private int[] GetRandomPosition()
        {
            int min = 0;
            int max = 4;
            Random random = new Random();
            int x = random.Next(min, max);
            int y = random.Next(min, max);
            return new int[] { x, y };
        }

        //Given a matrix, return list of empty coordinates
        private List<int[]> GetEmptyBlocks(int[,] matrix)
        {
            List<int[]> blocks = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        int[] temp = { i, j };
                        blocks.Add(temp);
                    }
                }
            }
            return blocks;
        }

        //Given a list of empty positions, pick one of the coordinates
        private int[] SelectPosition(List<int[]> emptyBlocks)
        {
            Random random = new Random();
            int x = random.Next(0, emptyBlocks.Count);
            return emptyBlocks[x];
        }
    }


}
