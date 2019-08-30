using System;
namespace Knight
{
    class Program
    {
        static void solveProblem(bool debug){
            Random random = new Random();
            int initX= random.Next(0, 10);
            int initY = random.Next(0, 10);
            Console.WriteLine("Inital Position: " + initX + "," + initY);

            int[,] scoreBoard = new int[10,10];
            Knight knight=new Knight(initX,initY);
            if(debug){
                knight.debug=true;
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            scoreBoard=knight.makeScoreBoard(scoreBoard);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Solution found on: " + elapsedMs + " ms");
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
