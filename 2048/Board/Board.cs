using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2048.Board
{
    public class Board
    {
        public int Score { get; set; }
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
            ShowScore();
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
                    GoLeft(ref board);
                    GenerateNewBlock(ref board);
                    goto WAITUSER;
                case ConsoleKey.RightArrow:
                    GoRight(ref board);
                    GenerateNewBlock(ref board);
                    goto WAITUSER;
                case ConsoleKey.Home:
                    return;
                default:
                    goto WAITUSER;
            }
        }
        private static void PrintBoard(int[,] matrix)
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
        private static void GoDown(ref int[,] board)
        {
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
        private static void GoUp(ref int[,] board)
        {
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
        private static void GoLeft(ref int[,] board)
        {
            for (int i = 0; i < 4; i++)
            {
                var temp = 0;
                for (int j = 3; j > -1; j--)
                {
                    if (j == 0)
                    {
                        continue;
                    }
                    if (board[i, j] == 0)
                    {
                        continue;
                    }
                    if (board[i, j - 1] == 0)
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i, j - 1] = temp;
                    }
                    if (board[i, j - 1] == board[i, j])
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i, j - 1] += temp;
                    }
                }
            }
        }
        private static void GoRight(ref int[,] board)
        {
            for (int i = 0; i < 4; i++)
            {
                var temp = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (j == 3)
                    {
                        continue;
                    }
                    if (board[i, j] == 0)
                    {
                        continue;
                    }
                    if (board[i, j + 1] == 0)
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i, j + 1] = temp;
                    }
                    if (board[i, j + 1] == board[i, j])
                    {
                        temp = board[i, j];
                        board[i, j] = 0;
                        board[i, j + 1] += temp;
                    }
                }
            }
        }

        //Show the results of the game
        private void ShowScore(bool win = false, bool lose = false)
        {
            if (win)
            {
                Console.WriteLine("CONGRATS! You reached 2048!");
                Console.WriteLine("SCORE: " + Score);
            }
            else if (lose)
            {
                Console.WriteLine("Well.. Looks like you can't reach 2048 right now. Maybe try again? Press HOME to exit.");
                Console.WriteLine("SCORE: " + Score);
            WAIT:
                if (Console.ReadKey().Key == ConsoleKey.Home)
                {
                    Environment.Exit(0);
                }
                else goto WAIT;
            }
            Console.WriteLine();
            Console.WriteLine("SCORE: " + Score);
        }

        //Given a matrix, generate a new number in empty block | 90% chance to spawn 2 and 10% 4
        private int[,] GenerateNewBlock(ref int[,] matrix)
        {
            //Check if player already won
            if (CheckWin(ref matrix))
            {
                ShowScore(true);
            }
            //Generate list of empty positions
            var emptyBlocks = GetEmptyBlocks(matrix);
            if (emptyBlocks.Count == 0)
            {
                //Return end game if not able to generate new number
                ShowScore(false, true);
                return matrix;
            }
            //Select empty coordinate
            var coordinate = SelectPosition(emptyBlocks);
            //Generate new number on that position
            int newNumber = GetNumber();
            matrix[coordinate[0], coordinate[1]] = newNumber;
            //Add number to score
            Score += newNumber;
            //Return updated matrix;
            return matrix;
        }

        //Check if the block 2048 exists
        private static bool CheckWin(ref int[,] matrix)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Generate number based on probabilities
        private static int GetNumber()
        {
            Random random = new();
            const double margin = 90.0 / 100.0;
            return random.NextDouble() <= margin ? 2 : 4;
        }

        //Given a matrix, return list of empty coordinates
        private static List<int[]> GetEmptyBlocks(int[,] matrix)
        {
            List<int[]> blocks = new();
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
        private static int[] SelectPosition(List<int[]> emptyBlocks)
        {
            Random random = new();
            int x = random.Next(0, emptyBlocks.Count);
            return emptyBlocks[x];
        }
    }


}
