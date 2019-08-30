using System;
using System.Collections.Generic;

namespace Knight
{
    class Knight{
        public int posX,posY, steps;                            //Positions of knight and the number of steps it has taken
        public List<int[]> history = new List<int[]>();         //The history of the cells visited
        public bool debug=false;
        public Knight(int posX,int posY){                       //knight instantiation
            this.steps=0;
            this.posX=posX;
            this.posY=posY;
            history.Add(new int[]{posX,posY});
        }
        public String whereAmI(){
            return "("+this.posX+","+this.posY+")";
        }
        public void printBoard(int[,] score){                                                       //prints the board
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
        public void printRoute(){                                                                   //prints the history route
            int stepCount=0;
            string result="";
            foreach (int[] step in this.history)
            {
                result += "step " + stepCount + " : " + step[0] + "," + step[1] + "\n";
                stepCount++;
            }
            Console.WriteLine(result);
        }
        public void moveKnight(int posX,int posY){                                                  //repositions the knight
            this.posX=posX;
            this.posY=posY;
        }
        public bool checkBounds(int moveX,int moveY){                                               //checks if a cell is outside the bounds of the board
            // Console.WriteLine("bounds of" +moveX+","+moveY);                                     //fixed on 10x10
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
        public List<int[]> checkRight(int[] now){                                                   //this two methods check for all the possible moves                                                                      
            List<int[]> nextPos=new List<int[]>();                                                  //a knight can do taking in account the bounds of the
            if(checkBounds(now[0]+1,now[1]+2)){                                                     //board and the cells already visited
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
        public Tuple<List<int[]>,int> nextMoves(int currentPosX, int currentPosY){                  //Method that receives the position of the knight and                       
            int[] now = new int[]{currentPosX,currentPosY};                                         //returns a list with the next possible positions and
            List<int[]> nextMovesR=checkRight(now);                                                 //the score for that cell
            List<int[]> nextMovesL=checkLeft(now);
            nextMovesR.AddRange(nextMovesL);
            if(this.debug){
                Console.WriteLine("Possible Positions for " +currentPosX +","+currentPosY);
                foreach (int[] pos in nextMovesR){
                    Console.Write("("+pos[0]+","+pos[1]+")");
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
            if(history.Count>=100){                                                                 //solution found all cells have been visited
                //mark last cell
                score[this.posX,this.posY]=steps;
                Console.WriteLine("solution found");
                printRoute();                                                                       //printing the final board with the number cells in step order                                                                   
                printBoard(score);                                                                  //with the path
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
            
            int minScore=10;                                                                        //setting the score for the moves
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
            makeScoreBoard(score);                                                                  //We continue for the next new position
            return score;
        }
    }
}