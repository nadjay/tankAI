using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank_1.AI
{
    class PathFinder
    {
        Map clientMap;
        const int width = Map.MAP_WIDTH;
        const int height = Map.MAP_HEIGHT;
        Node[,] map = new Node[width, height];
        int startX, startY, endX, endY;
        List<Node> openList, closedList;
        Game_AI.Mode mode;
        Stack<Point> path;
        int pathLength;
        DirectionConstant currentDirection;
        bool path_calculated;

        public PathFinder(Map map)
        {
            clientMap = map;
            openList = new List<Node>();
            closedList = new List<Node>();
            mode = Game_AI.Mode.Greedy;
        }

        //genarating the client map which contains nodes

        public void generateMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Actor actor = clientMap.getActor(i, j);
                    Node node = new Node();
                    node.X = i;
                    node.Y = j;
                    if (actor != null)
                    {
                        if (actor.GetType() == typeof(Brick))
                        {
                            node.Type = NodeType.brick;
                            node.Walkable = true;
                        }
                        else if (actor.GetType() == typeof(Player))
                        {
                            node.Type = NodeType.tank;
                            node.Walkable = true;
                        }
                        else if (actor.GetType() == typeof(Coin))
                        {
                            node.Type = NodeType.coinPile;
                            node.Walkable = true;
                        }
                        else if (actor.GetType() == typeof(Stone))
                        {
                            node.Type = NodeType.stone;
                            node.Walkable = false;
                        }
                        else if (actor.GetType() == typeof(LifePack))
                        {
                            node.Type = NodeType.lifePack;
                            node.Walkable = true;
                        }
                        else if (actor.GetType() == typeof(Water))
                        {
                            node.Type = NodeType.water;
                            node.Walkable = false;
                        }
                    }
                    else
                    {
                        node.Type = NodeType.empty;
                        node.Walkable = true;
                    }
                    map[i, j] = node;
                }
            }
        }

        public Stack<Point> findShortestPath()
        {
            openList.Clear();
            closedList.Clear();
            path_calculated = false;
            Stack<Node> path;
            currentDirection = clientMap.Client.getcDirection();

            switch (mode)
            {
                //revalue the node types in the context of each mode
                case Game_AI.Mode.Offensive:
                    NodeType.tank = 1;
                    NodeType.coinPile = 2;
                    NodeType.brick = 3;
                    NodeType.lifePack = 4;
                    break;
                case Game_AI.Mode.Greedy:
                    NodeType.coinPile = 1;
                    NodeType.lifePack = 2;
                    NodeType.brick = 3;
                    NodeType.tank = 4;
                    break;
                case Game_AI.Mode.Defensive:
                    NodeType.lifePack = 1;
                    NodeType.coinPile = 2;
                    NodeType.brick = 3;
                    NodeType.tank = 4;
                    break;
            }

            //generates the client map for path finding
            generateMap();

            startX = clientMap.Client.getxCordinate();
            clientMap.Client.getyCordinate();

            openList.Add(map[startX, startY]);

            while (!path_calculated)
            {
                analyzeNeighbours();
            }
            this.path = findPath();
            return path;
        }

        public void analyzeNeighbours()
        {
            if (openList.Count() == 0)
            {
                path_calculated = true;
            }
            Node currentNode = openList.ElementAt(0);
            if (!openList.Remove(currentNode))
            {
                throw new InvalidOperationException("Node not found in the list");
            }
            closedList.Add(currentNode);
            if (currentNode.X == endX && currentNode.Y == endY)
            {
                path_calculated = true;
            }
            if (currentNode.X - 1 >= 0)
            {
                Node neighbor = map[currentNode.X - 1, currentNode.Y];
                analyzeNode(currentNode, neighbor);
            }
            if (currentNode.X + 1 <= width)
            {
                Node neighbor = map[currentNode.X + 1, currentNode.Y];
                analyzeNode(currentNode, neighbor);
            }
            if (currentNode.Y - 1 >= 0)
            {
                Node neighbor = map[currentNode.X , currentNode.Y-1];
                analyzeNode(currentNode, neighbor);
            }
            if (currentNode.Y + 1 >= 0)
            {
                Node neighbor = map[currentNode.X , currentNode.Y+1];
                analyzeNode(currentNode, neighbor);
            }
            sortList(openList);
        }

        public void analyzeNode(Node current, Node neighbor){
            if (neighbor.Walkable)
        }
    }
}
