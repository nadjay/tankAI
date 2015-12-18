using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank_1
{
    class Game
    {
        public Player tank;
        public Map map;
        public AI.Game_AI AI;
        public AI.PathFinder path_finder;

        public Game()
        {
            this.tank = new Player();
            this.map = new Map(tank);
            this.AI = new AI.Game_AI(map);
            this.path_finder = new AI.PathFinder(map);
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            List<Coin> coinl = new List<Coin>();
            List<Stone> stonel = new List<Stone>();
            List<Brick> brickl = new List<Brick>();
            List<Water> waterl = new List<Water>();
            game.create_coin(2,0,1000,2000,coinl);
            //game.create_coin(1, 2, 1000, 2000, coinl);
            game.create_stone(1,0,stonel);
            game.create_stone(1, 2, stonel);
            game.create_stone(1,3,stonel);
            game.create_Brick(1,1,brickl);
            game.create_water(3,2,waterl);

            game.map.coinPiles = coinl;
            game.map.stones = stonel;
            game.map.bricks = brickl;
            game.map.waterPits = waterl;

            game.map.Client.setCordinates(0, 3);

            Console.WriteLine("coins: (" + coinl.ElementAt(0).getxCordinate() + "," + coinl.ElementAt(0).getyCordinate() + ")");
            Console.WriteLine("stones: (" + stonel.ElementAt(0).getxCordinate() + "," + stonel.ElementAt(0).getyCordinate() + ")");
            Console.WriteLine("bricks: (" + brickl.ElementAt(0).getxCordinate() + "," + brickl.ElementAt(0).getyCordinate() + ")");
            Console.WriteLine("water: (" + waterl.ElementAt(0).getxCordinate() + "," + waterl.ElementAt(0).getyCordinate() + ")");
            AI.Cell pos = new AI.Cell();
            pos.x = game.map.Client.getxCordinate();
            pos.y = game.map.Client.getyCordinate();
            Console.WriteLine("player pos : (" + pos.x + "," + pos.y + ")");
            game.AI.computeTarget(pos);
            Console.WriteLine("Target: ("+game.AI.Target.x +","+ game.AI.Target.y+")");
            Stack<AI.Cell> path = game.path_finder.calculateShortestPath(game.AI.Target, pos);
            Console.WriteLine("path found :" + game.path_finder.finished_calculation);
            if (path.Count != 1)
            {
                Console.WriteLine("path --->");
                while (path.Count > 0)
                {
                    AI.Cell cell = path.Pop();
                    int x = cell.x;
                    int y = cell.y;
                    Console.Write("(" + x + "," + y + ")");
                }

            }
            else
            {
                Console.WriteLine("------a path not found------");
            }
            Console.ReadLine();
            

        }

        public void create_coin(int x, int y, int t, int v, List<Coin>list)
        {
            Coin coin1 = new Coin();
            coin1.setCordinates(x,y);
            coin1.setTime(t);
            coin1.setValue(v);
            list.Add(coin1);
        }

        public void create_stone(int x, int y, List<Stone> list)
        {
            Stone stone = new Stone();
            stone.setCordinates(x,y);
            list.Add(stone);            
        }

        public void create_Brick(int x, int y, List<Brick> list)
        {
            Brick brick = new Brick();
            brick.setCordinates(x, y);
            list.Add(brick);
        }

        public void create_water(int x, int y, List<Water> list)
        {
            Water water = new Water();
            water.setCordinates(x, y);
            list.Add(water);
        }
       
    }
}
