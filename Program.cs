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
        public bool checkBounds(int posx,int posy){
            if(posX>9||posX<0||posy>9||posY<0){
                return true;
            }else{
                return false;
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
        public void nextMoves(){
            int[] now = new int[]{this.posX,this.posY};
            List<int[]> nextMoves=checkRight(now);
            foreach (int[] pos in nextMoves){
                Console.WriteLine(pos[0]+","+pos[1]);
            }
                
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
            Knight knight=new Knight(initX,initY);
            knight.nextMoves();
            Console.WriteLine("Hello World!");
                
            
        }
    }
}
