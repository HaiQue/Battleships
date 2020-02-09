using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Battleships
{
    class Program
    {

        static void Main(string[] args)
        {
            Helper helperObject = new Helper();
            helperObject.ShowWelcomeText();
            Console.WriteLine("Welcome To Battleships!");
            Console.WriteLine("Valid Coordinates are: Letters A-J, Numbers 0-9");
            Console.WriteLine("Coordinates example: A1");

            //Initialize real board and display board
            int[,] board = new int[10, 10];
            string[,] showBoard = new string[10, 10];

            //Fill display with neutral values
            helperObject.InitializeDisplayBoard(showBoard);


            Ship mother = new Ship() { Name = "Battleship", Width = 5, Marker = 1 };
            Ship destroyer = new Ship() { Name = "Destroyer", Width = 4, Marker = 2 };
            Ship destroyer1 = new Ship() { Name = "Destroyer1", Width = 4, Marker = 3 };
            List<Ship> ships = new List<Ship> { mother, destroyer, destroyer1 };


            //place ships randomly 
            helperObject.PlaceShips(board, ships);
            bool endGame = false;
            int numberOfShots = 0;

            do
            {
                numberOfShots += 1;
                int[] coord = helperObject.GetCoordinates();



                if (!Enumerable.Range(0, 9).Contains(coord[0]) || !Enumerable.Range(0, 9).Contains(coord[1]))
                {
                    Console.WriteLine($"Please enter valid coordinates!");
                    continue;
                }

                if (ships.AsEnumerable().Select(x => x.Width).Sum() != 0)
                {
                    var cell = board[coord[1], coord[0]];
                    if (cell == 0)
                    {
                        board[coord[1], coord[0]] = 9;
                        showBoard[coord[1], coord[0]] = "M";
                        Console.WriteLine("Miss!");
                    }
                    else if (cell == mother.Marker)
                    {

                        board[coord[1], coord[0]] = 9;
                        showBoard[coord[1], coord[0]] = "H";
                        Console.WriteLine("Hit!");
                        mother.Width = mother.Width - 1;
                        if (mother.Width == 0)
                        {

                            Console.WriteLine("The Ship Sunk!");
                        }
                    }
                    else if (cell == destroyer.Marker)
                    {
                        board[coord[1], coord[0]] = 9;
                        showBoard[coord[1], coord[0]] = "H";
                        Console.WriteLine("Hit!");
                        destroyer.Width = destroyer.Width - 1;
                        if (destroyer.Width == 0)
                        {

                            Console.WriteLine("The Ship Sunk!");
                        }
                    }
                    else if (cell == destroyer1.Marker)
                    {
                        board[coord[1], coord[0]] = 9;
                        showBoard[coord[1], coord[0]] = "H";
                        Console.WriteLine("Hit!");
                        destroyer1.Width = destroyer1.Width - 1;
                        if (destroyer1.Width == 0)
                        {

                            Console.WriteLine("The Ship Sunk!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You fired already on this spot!");
                    }



                    helperObject.ShowMatrice(showBoard);
                }
                else
                {
                    endGame = true;
                }
            } while (!endGame);

            Console.Write($"Congrats!You won! With {numberOfShots} shots! ");

        }

    }
}