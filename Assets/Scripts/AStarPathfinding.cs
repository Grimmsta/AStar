using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Grimm
{
    public class AStarPathfinding : MonoBehaviour
    {
        Node[,] grid;
        public List<Node> path;
        bool allowDiagonal;

        public bool FindPath(Node[,] grid, Vector2 startPos, Vector2 targetPos) //Is there a path between the target and the player
        {
            this.grid = grid;
            Node startNode = grid[(int)startPos.x, (int)startPos.y]; //The startnode is the position of the player
            Node targetNode = grid[(int)targetPos.x, (int)targetPos.y]; //The target node is the position of the apple

            List <Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) //If the open list has a index higher than 0
            {
                Node currentNode = openSet[0]; //Current node is the first one in the open index

                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) //if the node in the open set is cheaper than current node
                    {
                        currentNode = openSet[i]; //the node in the open set is the new current node
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode) //if the current node has the same coordinates as the target node, it means we found a path
                {
                    RetracePath(startNode, targetNode);
                    return true;
                }

                foreach (Node neighbour in GetNeighbour(currentNode))
                {
                    if (neighbour.walkable == false || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) //If g cost is less than the neigbours g cost or openset doesn't contain neigbour
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(currentNode, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }return false;
        }

        private void RetracePath(Node start, Node end) //We backtrack the path, since the end end is the starting pos
        {
            path = new List<Node>();
            Node current = end;

            while (current != start)
            {
                path.Add(current);
                current = current.parent;
            }
            path.Reverse();
        }

        int GetDistance (Node a, Node b) //Gets the distance between node a and b
        {
            //allowDiagonal = true;
            int distX = Mathf.Abs(a.x - b.x);
            int distY = Mathf.Abs(a.y - b.y);

            return distX + distY;

            #region Other method of calculating
            //if (allowDiagonal)
            //{
            //    if (distX > distY)
            //    {
            //        return 14 * distX + 10 * (distY - distX);
            //    }

            //    return 14 * distY + 10 * (distX - distY);
            //}

            //if (distX > distY)
            //{
            //    return  distX + 1* (distY - distX);
            //}

            //return distY + 1 * (distX - distY);
            #endregion 
        }

        public List<Node> GetNeighbour(Node tile) //Gets all nodes surrounding you
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (allowDiagonal)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (x == 0 && y ==0 || x == 1 && y == 1|| x == -1 && y == 1 || x == 1 && y == -1 || x == -1 && y == -1)
                        {
                            continue;
                        }
                    }

                    int checkX = tile.x + x;
                    int checkY = tile.y + y;

                    if (checkX > -1 && checkX < grid.GetLength(0) && checkY > -1 && checkY < grid.GetLength(1))
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }
    }
}
