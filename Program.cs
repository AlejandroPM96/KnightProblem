using System;
using System.Collections.Generic;

namespace Knight
{
    class Knight{
        public int posX,posY;
        public List<int[]> history = new List<int[]>();
        public Knight(int posX,int posY){
            this.posX=posX;
            this.posY=posY;
            history.Add(new int[]{posX,posY});
        }
        public String whereAmI(){
            return "("+this.posX+","+this.posY+")";
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
            Console.WriteLine("Next Pos for" +currentPosX +","+currentPosY);
            List<int[]> nextMovesR=checkRight(now);
            List<int[]> nextMovesL=checkLeft(now);
            nextMovesR.AddRange(nextMovesL);
            foreach (int[] pos in nextMovesR){
                Console.WriteLine(pos[0]+","+pos[1]);
            }
            int cellScore = nextMovesR.Count;
            Console.WriteLine(cellScore);
            return Tuple.Create<List<int[]>,int>(nextMovesR,cellScore);
        }
        public int[,] makeScoreBoard(int[,] score){
            Tuple<List<int[]>,int> result = this.nextMoves(this.posX,this.posY);
            List<int[]> nextMoves = result.Item1;
            int[] bestNextPosition =new int[2];
            int cellScore = result.Item2;
            score[this.posX,this.posY]=-1;
            int minScore=10;
            foreach (int[] newPos in nextMoves)
            {
                Tuple<List<int[]>,int> resultAux = this.nextMoves(newPos[0],newPos[1]);
                // List<int[]> nextMovesAux = result.Item1;
                int cellScoreAux = resultAux.Item2;
                if(cellScoreAux < minScore){
                    minScore=cellScoreAux;
                    bestNextPosition = new int[]{newPos[0],newPos[1]};
                }
                score[newPos[0],newPos[1]]=cellScoreAux;
            }
            Console.WriteLine("next position=> " + bestNextPosition[0]+","+bestNextPosition[1]);
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
            
            Knight knight=new Knight(initX,initY);
            scoreBoard=knight.makeScoreBoard(scoreBoard);
            Console.WriteLine("Hello World!");
            
            
            for (int i = 0; i < scoreBoard.GetLength(0); i++) {  
                for (int j = 0; j <scoreBoard.GetLength(0); j++) {
                    if(scoreBoard[i, j]!=0){
                        if(scoreBoard[i, j]==-1){
                            Console.Write("i" + " ");
                        }else{
                            Console.Write(scoreBoard[i, j] + " "); 
                        }
                    }else{
                        Console.Write("- ");
                    }
                }  
                Console.WriteLine("\n");
            }
            
        }
    }
}
