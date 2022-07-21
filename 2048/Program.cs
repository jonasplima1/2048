using System;
using System.Threading;
using C2048.Board;

START:
Console.WriteLine("Welcome to 2048 C#! Press ENTER to start playing or HOME to get some help.");
var output = Console.ReadKey().Key;
Thread.Sleep(100);
if (output == ConsoleKey.Enter)
{
    //Calls method to start game;
    Board Board = new Board();
    Board.StartGame();
} 
else if (output == ConsoleKey.Home)
{
    Thread.Sleep(100);
    Console.WriteLine("-----------------------");
    Console.WriteLine("HELP!");
    Console.WriteLine("-----------------------");
    Console.WriteLine("The objective here is to build a block with the number 2048 by moving the board around");
    Console.WriteLine("You can do that by:");
    Console.WriteLine("Press ↓, to push the board down");
    Console.WriteLine("Press ↑, to push the board up");
    Console.WriteLine("And... Well, you get it.");
    Console.WriteLine("Or you can use the force. Remember that the numbers always sums up.");
    Console.WriteLine("If you want to quit or restart the game, press HOME! Try it right now.");
WAITING:
    var outputHelp = Console.ReadKey().Key;
    if (outputHelp == ConsoleKey.Home)
    {
        Console.Clear();
        goto START;
    }
    else
    {
        Console.WriteLine("I said HOME!");
        goto WAITING;
    }
}
else
{
    Console.Clear();
    goto START;
}





