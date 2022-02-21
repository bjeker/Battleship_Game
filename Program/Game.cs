using System;

namespace Program
{
    public class Game
    {

        //new cheat board and real board
        Gameboard board = new Gameboard();
        Gameboard realBoard = new Gameboard();
        Gameboard shipBoard = new Gameboard();
        
        //array to store ships
        Ship[] ships = new Ship[6];

        public void ShipInitialize()
        {
            //ship objects
            Ship Destroyer_1 = new Ship("Destroyer 1", 2);
            Ship Destroyer_2 = new Ship("destroyer 2", 2);
            Ship Submarine_1 = new Ship("Submarine 1", 3);
            Ship Submarine_2 = new Ship("submarine 2", 3);
            Ship Battleship = new Ship("Battleship", 4);
            Ship Carrier = new Ship("Carrier", 5);

            //add ships to array
            ships[0] = Destroyer_1;
            ships[1] = Destroyer_2;
            ships[2] = Submarine_1;
            ships[3] = Submarine_2;
            ships[4] = Battleship;
            ships[5] = Carrier;
        }

        //sets up ships for the grid
        public void SetupShips()
        {
            for (int i = 0; i < ships.Length; i++)
            {
                //stores if legal placement
                bool legal = true;
                while(legal)
                {
                    //randomizes direction
                    Random rand = new Random();
                    int direction = rand.Next(0, 2);

                    //sets to overlap
                    bool overlapping = true;
                    while(overlapping)
                    {
                        //randomizes x and y for ship
                        ships[i].X = rand.Next(0, 10);
                        ships[i].Y = rand.Next(0, 10);

                        //sets value for overlapping to passed function
                        overlapping = Overlap(i, direction);

                    }

                    //if x direction
                    if (direction == 0)
                    {
                        if (ships[i].X + ships[i].Length - 1 < board.Grid.GetLength(0))
                        {
                            for (int j = ships[i].X; j < ships[i].X + ships[i].Length; j++)
                            {
                                //sets char to first letter of the name of the ship
                                board.SetChar(j, ships[i].Y, ships[i].Name[0]);
                                shipBoard.SetChar(j, ships[i].Y, 'S');
                                ships[i].SetPosition(j, ships[i].Y);
                            }

                            legal = false;
                        }
                    } 
                    
                    //if y direction
                    else if(direction == 1)
                    {
                        if (ships[i].Y + ships[i].Length - 1 < board.Grid.GetLength(0))
                        {
                            for (int j = ships[i].Y; j < ships[i].Y + ships[i].Length; j++)
                            {
                                //same as done above for x
                                board.SetChar(ships[i].X, j, ships[i].Name[0]);
                                shipBoard.SetChar(ships[i].X, j, 'S');
                                ships[i].SetPosition(ships[i].X, j);
                            }

                            legal = false;
                        }
                    }
                }
            }
        }

        //overlapping function using position and direction
        public bool Overlap(int position, int direction)
        {
            //direction is x
            if (direction == 0)
            {
                if (ships[position].X + ships[position].Length - 1 < board.Grid.GetLength(0))
                {
                    for (int j = ships[position].X; j < ships[position].X + ships[position].Length; j++)
                    {
                        //checks for open space
                        if (board.Grid[j, ships[position].Y] != ' ')
                        {
                            return true;
                        }
                    }
                }
            }

            //same as above but checks y direction
            else if (direction == 1)
            {
                if (ships[position].Y + ships[position].Length - 1 < board.Grid.GetLength(1))
                {
                    for (int j = ships[position].Y; j < ships[position].Y + ships[position].Length; j++)
                    {
                        if (board.Grid[ships[position].X, j] != ' ')
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //runs the game
        public void Run()
        {
            //conditions for win and cheats
            bool win = false;
            bool cheats = false;
            //round counter
            int rounds = 1;

            //initialize and fill the grid with ships
            ShipInitialize();
            board.FillGrid();
            realBoard.FillGrid();
            SetupShips();

            //intro
            Console.WriteLine("\t\tWELCOME TO BATTLESHIP COMMANDER");
            Console.WriteLine("\t\t-------------------------------");

            Console.WriteLine("Would you like to enable cheats? Type YES for cheats or NO for no cheats.");
            //check for cheats
            if (Console.ReadLine() == "YES")
            {
                cheats = true;
                shipBoard.Display();
            }
            else
            {
                cheats = false;
                realBoard.Display();
            }

            //loop until game is won
            while (!win)
            {
                //display rounds and start the turn
                Console.WriteLine("ROUND " + rounds );
                PlayerTurn();
                //display board based on cheats or not
                if (cheats == true)
                {
                    shipBoard.Display();
                }
                else
                {
                    realBoard.Display();
                }

                rounds++;

                //all ships are sunk then game is won
                if (ships[0].Health == 0 && ships[1].Health == 0 && ships[2].Health == 0 && ships[3].Health == 0 && ships[4].Health == 0 && ships[5].Health == 0)
                {
                    win = true;
                }
            }
            Console.WriteLine("YOUVE WON WOW!");
            Console.WriteLine("Do you want to play again? Type YES to play again");
            //runs game again if yes
            if (Console.ReadLine() == "YES")
            {
                Run();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }

        }

        public void PlayerTurn()
        {
            //prompt for location
            Console.WriteLine("What's our Target?");
            string target = Console.ReadLine();
            //first letter of ships name
            char shipName;

            //check if open space
            if (board.Grid[target[0] - 65, int.Parse(target.Substring(1)) - 1] == ' ')
            {
                //set miss char
                board.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'O');
                realBoard.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'O');
                shipBoard.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'O');
            }
            else
            {
                //switch case to check ships on cheat and real board and decrement ships health
                shipName = board.Grid[target[0] - 65, int.Parse(target.Substring(1)) - 1];

                switch (shipName)
                {
                    case 'D':
                        ships[0].Health--;
                        if (ships[0].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[0].Name}!");
                        }
                        break;
                    case 'd':
                        ships[1].Health--;
                        if (ships[1].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[1].Name}!");
                        }
                        break;
                    case 'S':
                        ships[2].Health--;
                        if (ships[2].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[2].Name}!");
                        }
                        break;
                    case 's':
                        ships[3].Health--;
                        if (ships[3].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[3].Name}!");
                        }
                        break;
                    case 'B':
                        ships[4].Health--;
                        if (ships[4].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[4].Name}!");
                        }
                        break;
                    case 'C':
                        ships[5].Health--;
                        if (ships[5].Health == 0)
                        {
                            Console.WriteLine($"You sunk my {ships[5].Name}!");
                        }
                        break;

                }

                //display hit char on cheat and real board
                board.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'X');
                realBoard.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'X');
                shipBoard.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'X');
            }

        }

    }

}