/*
    Luis Alejandro Peña Montoya A01650535
    https://github.com/AlejandroPM96/KnightProblem
    -Problem
        
    -Environment Analysis:
        -Fully observable: The entire board and its contents can be seen at any moment by the agent
        -Stochastic: The next move for the knight depends on the best available for the next positions which is
                    calculated by the agent.
        -Episodic: Each time the knight moves it calculates new positions based on the score of the next ones
                    and its one turn at a time
        -Static: While the knight calculates his next move the environment can not change
        -Discrete: The system moves through the steps the knight takes on the chess board
        -Known: we know the movements of the knight and the rules it has to follow
 */
using System;
namespace Knight
{
    class MainProgram
    {
        static void solveProblem(bool debug){
            Random random = new Random();
            int initX= random.Next(0, 10);
            int initY = random.Next(0, 10);
            Console.WriteLine("Initial Position: " + initX + "," + initY);

            int[,] scoreBoard = new int[10,10];
            Knight knight=new Knight(initX,initY);
            if(debug){
                knight.debug=true;
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            scoreBoard=knight.makeScoreBoard(scoreBoard);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Solution for ("+ initX + "," + initY+") found on: " + elapsedMs + " ms");
            showInstructions();

        }
        static void showInstructions(){
            Console.WriteLine("x - end program");
            Console.WriteLine("s - start program");
            Console.WriteLine("w - start program with step by step positioning");
        }
        static void Main(string[] args)
        {
            int Choice = -1;
            showInstructions();

            while ( Choice == -1 ){
                ConsoleKeyInfo currentKey = Console.ReadKey(true);
                switch ( currentKey.Key )
                {
                    case ConsoleKey.X:                                  // make method call here to handle "X"
                        Choice = 0;
                        break;
                    case ConsoleKey.S:                                  // make method call here to handle "S"
                        solveProblem(false);
                        break;
                    case ConsoleKey.W:                                  // make method call here to handle "W"
                        solveProblem(true);
                    break;
                    default:
                        break;
                } 
            }

            

            // printBoard(scoreBoard);
            // scoreBoard=knight.makeScoreBoard(scoreBoard);
            // printBoard(scoreBoard);
            
            
        }
    }
}
