using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Battleships
{
    class Helper
    {

        public void InitializeDisplayBoard(string[,] showBoard)
        {
            for (int x = 0; x < showBoard.GetLength(0); x++)
            {
                for (int y = 0; y < showBoard.GetLength(1); y++)
                {
                    showBoard[x, y] = "0";
                }
            }

        }


        public void PlaceShips(int[,] board, List<Ship> Ships)
        {

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {

                bool ifOccupied = true;
                while (ifOccupied)
                {
                    var startcolumn = rand.Next(0, 9);
                    var startrow = rand.Next(0, 9);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 100) % 2;

                    if (orientation == 0)
                    {
                        for (int i = 0; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //Check if outside of the board
                    if (endrow > 9 || endcolumn > 9)
                    {
                        ifOccupied = true;
                        continue;
                    }

                    //Check if spaces are occupied
                    var cellToCheck = GetSpace(board, startrow, startcolumn, endrow, endcolumn);
                    if (cellToCheck.Any(x => x != 0))
                    {
                        ifOccupied = true;
                        continue;
                    }
                    if (startrow < endrow)
                    {
                        for (int i = startrow; i < endrow; i++)
                        {
                            board[i, startcolumn] = ship.Marker;
                        }


                    }

                    if (startcolumn < endcolumn)
                    {
                        for (int i = startcolumn; i < endcolumn; i++)
                        {
                            board[startrow, i] = ship.Marker;
                        }
                    }

                    ifOccupied = false;
                }
            }
        }

        private List<int> GetSpace(int[,] boar, int startrow, int startcolumn, int endrow, int endcolumn)
        {
            List<int> resList = new List<int>();
            if (startrow < endrow)
            {
                for (int i = startrow; i <= endrow; i++)
                {
                    resList.Add(boar[i, startcolumn]);
                }


            }

            if (startcolumn < endcolumn)
            {
                for (int i = startcolumn; i <= endcolumn; i++)
                {
                    resList.Add(boar[startrow, i]);
                }
            }

            return resList;
        }

        public void ShowMatrice(string[,] board)
        {
            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }


        }


        public int[] GetCoordinates()
        {
            Console.Write("Enter coordinates: ");
            var inputValue = Console.ReadLine();
            int[] array = new int[2];
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"A", 0}, {"B", 1}, {"C", 2}, {"D", 3}, {"E", 4}, {"F", 5}, {"G", 6}, {"H", 7}, {"I", 8}, {"J", 9}
            };
            array[0] = dict.FirstOrDefault(x => x.Key == inputValue.ToCharArray()[0].ToString()).Value;
            array[1] = Convert.ToInt32(inputValue.ToCharArray()[1]) - 49;

            return array;

        }


        public void ShowWelcomeText()
        {
            Console.WriteLine(@"
        _           _   _   _           _     _       
       | |         | | | | | |         | |   (_)      
       | |__   __ _| |_| |_| | ___  ___| |__  _ _ __   ___
       | '_ \ / _` | __| __| |/ _ \/ __| '_ \| | '_ \ / __|
       | |_) | (_| | |_| |_| |  __/\__ \ | | | | |_) |\__\
       |_.__/ \__,_|\__|\__|_|\___||___/_| |_|_| .__/ |___/
                                               | |    
                                               |_|       ");

        }
    }
}