using System;
using System.Collections.Generic;

namespace Knight
{
    class Knight{
        public int posX,posY;
        public List<int[]> history;
        public Knight(int posX,int posY){
            this.posX=posX;
            this.posY=posY;
        }
        public String whereAmI(){
            return "("+this.posX+","+this.posY+")";
        }
        public bool checkBounds(int moveX,int moveY){
            //Console.WriteLine("bounds of" +moveX+","+moveY); 
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
                // Console.WriteLine("agregado");
                // Console.WriteLine((now[0]+1)+","+(now[1]+2));    
            }
            if(checkBounds(now[0]+1,now[1]-2)){
                nextPos.Add(new int[]{now[0]+1,now[1]-2});
                // Console.WriteLine("agregado");
                // Console.WriteLine((now[0]+1)+","+(now[1]+2));       
            }
            if(checkBounds(now[0]+2,now[1]+1)){
                nextPos.Add(new int[]{now[0]+2,now[1]+1});   
                // Console.WriteLine("agregado");
                // Console.WriteLine((now[0]+1)+","+(now[1]+2));    
            }
            if(checkBounds(now[0]+2,now[1]-1)){
                nextPos.Add(new int[]{now[0]+2,now[1]-1});    
                // Console.WriteLine("agregado");
                // Console.WriteLine((now[0]+1)+","+(now[1]+2));   
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
        public void nextMoves(){
            int[] now = new int[]{this.posX,this.posY};
            Console.WriteLine(now[0]+","+now[1]);
            List<int[]> nextMovesR=checkRight(now);
            List<int[]> nextMovesL=checkLeft(now);
            nextMovesR.AddRange(nextMovesL);
            foreach (int[] pos in nextMovesR){
                Console.WriteLine(pos[0]+","+pos[1]);
            }
            int cellScore = nextMovesR.Count;
            Console.WriteLine(cellScore);
        }
        public int[,] makeScoreBoard(int[,] score){
            
            
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
            knight.nextMoves();
            Console.WriteLine("Hello World!");
                
            
        }
    }
}
