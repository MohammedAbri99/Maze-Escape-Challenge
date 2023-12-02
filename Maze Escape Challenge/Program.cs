using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace Maze_Escape_Challenge
{
    internal class Program
    {
        // the varibels declear it here to be public for methods nesteed of send it as parameters.
        static string[,] maze;
        static int x_points;
        static int y_points;

        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                x_points = 1;
                y_points = 1;
                //by generateMaze() this function we intialize the maze.
                generateMaze();
                Console.WriteLine("Welcome to the Maze Escape Challenge!\n");
                Console.WriteLine("Generated Maze:");
                display();

                Console.WriteLine("\nUse W, E, S, N to move. Your goal is to reach the Exit (E)!");
                Console.WriteLine($"Current Position: ({x_points},{y_points})");

                /*
                 * since the user doesn't arrive to the goal(Exit) of the maze the game still working.
                 * first intialize choise variable to save the user input and then validate the input 
                 * if it's true the program move up to make the movement and if the input was wrong:
                 * user input was wrong or we goes to obsticals the program will give user the message to
                 * the correct option. 
                 */

                int s = 0; // to compute the step to reach the goal.
                while( maze[x_points,y_points] != "E" ) 
                {
                    string choise;
                    do
                    {
                        Console.Write("Enter your move (E/W/S/N): ");
                        choise = Console.ReadLine().ToLower();
                    } while (!isvalid(choise));
                    PlayerMovement(choise);
                    display();
                    Console.WriteLine($"Current Position: ({x_points},{y_points})");
                    s++;
                }
                Console.WriteLine();
                Console.WriteLine($"Congratulations! You've reached the Exit (E) in {s} moves!");

                string playingAgin;
                do
                {
                    Console.WriteLine("Do you want to play again? (Y/N): ");
                    playingAgin = Console.ReadLine().ToLower();
                } while (playingAgin!="n" && playingAgin != "y") ;
                flag = playingAgin=="y"?true:false;
            }
            Console.WriteLine("Thank you for playing the Maze Escape Challenge!");
        }
        static void generateMaze()
        {
            maze = new string[5,7] { 
                { "0","0","0","0","0","0","0"},
                { "0","0","0","0","0","0","0"},
                { "0","0","0","0","0","0","0"},
                { "0","0","0","0","0","0","0"},
                { "0","0","0","0","0","0","0"}};
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == 0 || i == 5 - 1 || j == 0 || j == 7 - 1)
                    {
                        maze[i, j] = "#";
                    }
                    else
                    {
                        maze[i, j] = " ";
                    }
                }
            }
            /*
             * the function of comming code is to make the obsticals iside the maze.()
             */
            Random random = new Random();
            int k = 0; 
            while ( k <3)
            {
                int x = random.Next(1, 4);
                int y = random.Next(1, 6);
                // if the random point is the start point or the crosisng the raod the lead to exit(E).
                // actully there is a little possiblity to cross it.
                bool flag = (x==1 && y==1) || (x==3 && y==5);
                if(!flag) 
                { 
                    maze[x, y] = "#";
                    k++;
                }
            }

            maze[4, 5] = "E";
        }

        // after we check the user input and ensure the input is right so make the movement by this function(PlayerMovement).
        static void PlayerMovement(string movestep)
        {
            switch (movestep.ToLower())
            {
                case "w":
                    y_points -= 1;
                    break;
                case "e":
                    y_points += 1;
                    break;  
                case "n":
                    x_points -= 1;
                    break;
                case "s":
                    x_points += 1;
                    break;
                default: break;
            }
        }

        /*
         * function display() to display the maze after each movement based on the movement step 
         * so if the point where the user arrive the display S and the other points will display
         * it as it in the original maze.
         */
        static void display()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (x_points == i && y_points == j)
                    {
                        Console.Write("S");
                    }
                    else
                    {
                        Console.Write(maze[i,j]);
                    }
                }
                Console.WriteLine();
            }
        }

        /*
         * the core of the program is in this function. 
         * to be user input valid we need to check:
         *  - user input is between 0 and lenght of row.
         *  - user input is between 0 and lenght of column.
         *  - also it should not an obisticals (#).
         *  if these things is true it will return true.
         */
        static bool isvalid(string movestep) 
        {
            switch (movestep.ToLower())
            {
                case "w":
                    return (y_points - 1) >= 0 && x_points >= 0 && x_points < maze.GetLength(0) && (y_points-1) < maze.GetLength(1) && maze[x_points, (y_points-1)] != "#";
                case "e":
                    return (y_points + 1) >= 0 && x_points >= 0 && x_points < maze.GetLength(0) && (y_points+1) < maze.GetLength(1) && maze[x_points, (y_points+1)] != "#";
                case "n":
                    return (x_points-1) >= 0 && y_points >= 0 && (x_points-1) < maze.GetLength(0) && y_points < maze.GetLength(1) && maze[(x_points-1), y_points] != "#";
                case "s":
                    return (x_points+1) >= 0 && y_points >= 0 && (x_points+1) < maze.GetLength(0) && y_points < maze.GetLength(1) && maze[(x_points+1), y_points] != "#";
                default:
                    return false;
            }

        }
    }
}
