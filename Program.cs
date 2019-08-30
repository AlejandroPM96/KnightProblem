using System;
using System.Collections.Generic;

namespace Knight
{
    class Knight{
        public int posX,posY, steps;
        public List<int[]> history = new List<int[]>();
        public bool debug=false;
        public Knight(int posX,int posY){
            this.steps=0;
            this.posX=posX;
            this.posY=posY;
            history.Add(new int[]{posX,posY});
        }
        public String whereAmI(){
            return "("+this.posX+","+this.posY+")";
        }
        public void printBoard(int[,] score){
            for (int i = 0; i < score.GetLength(0); i++) {  
                for (int j = 0; j <score.GetLength(0); j++) {
                    if(score[i, j]!=0){
                        if(score[i, j]==-1){
                            Console.Write("i" + " \t");
                        }else{
                            Console.Write(score[i, j] + " \t"); 
                        }
                    }else{
                        Console.Write("- ");
                    }
                }  
                Console.WriteLine("\n");
            }
        }
        public void printRoute(){
            int stepCount=0;
            string result="";
            foreach (int[] step in this.history)
            {
                result += "step " + stepCount + " : " + step[0] + "," + step[1] + "\n";
                stepCount++;
            }
            Console.WriteLine(result);
        }
        public void moveKnight(int posX,int posY){
            this.posX=posX;
            this.posY=posY;
        }
        public bool checkBounds(int moveX,int moveY){
            // Console.WriteLine("bounds of" +moveX+","+moveY);
            //check if de position has already been visited
            int[] desiredPos = new int[]{moveX,moveY};
            foreach (int[] posObject in history)
            {
                if(desiredPos[0]==posObject[0]&&desiredPos[1]==posObject[1]){
                    return false;
                }
            }
            //check bounds
            if(moveX>=10||moveX<0||moveY>=10||moveY<0){
                return false;
            }else{
                return true;
            }
        }
        public List<int[]> checkRight(int[] now){
            List<int[]> nextPos=new List<int[]>();
            if(checkBounds(now[0]+1,now[1]+2)){
                nextPos.Add(new int[]{now[0]+1,now[1]+2});   
            }
            if(checkBounds(now[0]+1,now[1]-2)){
                nextPos.Add(new int[]{now[0]+1,now[1]-2});      
            }
            if(checkBounds(now[0]+2,now[1]+1)){
                nextPos.Add(new int[]{now[0]+2,now[1]+1});  
            }
            if(checkBounds(now[0]+2,now[1]-1)){
                nextPos.Add(new int[]{now[0]+2,now[1]-1});
            }
            return nextPos;
        }
        public List<int[]> checkLeft(int[] now){
            List<int[]> nextPos=new List<int[]>();
            if(checkBounds(now[0]-1,now[1]+2)){
                nextPos.Add(new int[]{now[0]-1,now[1]+2});    
            }
            if(checkBounds(now[0]-1,now[1]-2)){
                nextPos.Add(new int[]{now[0]-1,now[1]-2});    
            }
            if(checkBounds(now[0]-2,now[1]+1)){
                nextPos.Add(new int[]{now[0]-2,now[1]+1});    
            }
            if(checkBounds(now[0]-2,now[1]-1)){
                nextPos.Add(new int[]{now[0]-2,now[1]-1});    
            }
            return nextPos;
        }
        public Tuple<List<int[]>,int> nextMoves(int currentPosX, int currentPosY){
            int[] now = new int[]{currentPosX,currentPosY};
            
            List<int[]> nextMovesR=checkRight(now);
            List<int[]> nextMovesL=checkLeft(now);
            nextMovesR.AddRange(nextMovesL);
            if(this.debug){
                Console.WriteLine("Possible Positions for" +currentPosX +","+currentPosY);
                foreach (int[] pos in nextMovesR){
                    Console.Write(pos[0]+","+pos[1]);
                }
                Console.Write("\n");
            }
            int cellScore = nextMovesR.Count;
            return Tuple.Create<List<int[]>,int>(nextMovesR,cellScore);
        }
        public int[,] makeScoreBoard(int[,] score){
            if(this.debug){
                Console.WriteLine("Knight in " +whereAmI()+ "step: " + steps);
            }
            if(history.Count>=100){ //solution found
                //mark last cell
                score[this.posX,this.posY]=steps;
                Console.WriteLine("solution found");
                printRoute();
                //printing the final board with the number cells in step order
                printBoard(score);
                return score;
            }
            Tuple<List<int[]>,int> result = this.nextMoves(this.posX,this.posY);                    //Get the next possible moves for the current state
            List<int[]> nextMoves = result.Item1;
            int[] bestNextPosition =new int[2];
            int cellScore = result.Item2;
            if(this.steps==0){                                                                      // Just for marking on the board
                score[this.posX,this.posY]=-1;                                                      //
            }else{
                score[this.posX,this.posY]=this.steps;
            }
            
            int minScore=10;                                    //setting the score for the moves
            foreach (int[] newPos in nextMoves)                                                     //iterating the possible moves to find the lowest score
            {
                Tuple<List<int[]>,int> resultAux = this.nextMoves(newPos[0],newPos[1]);
                // List<int[]> nextMovesAux = result.Item1;
                int cellScoreAux = resultAux.Item2;
                if(cellScoreAux < minScore){
                    minScore=cellScoreAux;
                    bestNextPosition = new int[]{newPos[0],newPos[1]};
                }
            }
            //inserting new position
            score[bestNextPosition[0],bestNextPosition[1]]=minScore;                                //when found the knight is moved, the position moved is stored
            this.history.Add(bestNextPosition);
            moveKnight(bestNextPosition[0],bestNextPosition[1]);
            if(this.debug){
                Console.WriteLine("next position=> " + bestNextPosition[0]+","+bestNextPosition[1]);
            }
            this.steps++;
            makeScoreBoard(score);
            return score;
        }
    }
    
    class Program
    {

        static void Main(string[] args)
        {
            Random random = new Random();
            int initX= random.Next(0, 10);
            int initY = random.Next(0, 10);
            Console.WriteLine("pos inicial: " + initX + "," + initY);
            int[,] scoreBoard = new int[10,10];
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Knight knight=new Knight(initX,initY);
            scoreBoard=knight.makeScoreBoard(scoreBoard);
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;
            
            Console.WriteLine("Solution found on:" + elapsedMs + " ms");
            // printBoard(scoreBoard);
            // scoreBoard=knight.makeScoreBoard(scoreBoard);
            // printBoard(scoreBoard);
            
            // int Choice = -1;
            // while ( Choice == -1 ){
            //     ConsoleKeyInfo currentKey = Console.ReadKey(true);
            //     switch ( currentKey.Key )
            //     {
            //         case ConsoleKey.X:
            //             // make method call here to handle "W"
            //             Choice = 0;
            //             break;
            //         case ConsoleKey.N:
            //             // make method call here to handle "S"
            //             scoreBoard=knight.makeScoreBoard(scoreBoard);
            //             printBoard(scoreBoard);
            //             break;
            //         default:
            //             break;
            //     } 
            // }
        }
    }
}
