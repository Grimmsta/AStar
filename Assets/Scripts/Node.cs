using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimm
{
    //TODO: update here friday
    public class Node //Is used to store data 
    {
        public int x;
        public int y;
        public Vector2 worldPosition;
        public GameObject obj;

        //A*
        public bool walkable; //Can we walk on this node?
        public int gCost; //Distance from start node
        public int hCost; //Distance between current node and end node
        public Node parent;

        public Node(bool _walkable, Vector2 _worldPosition, int x, int y)
        {
            walkable = _walkable;
            worldPosition = _worldPosition;
            this.x = x;
            this.y = y;
        }

        public int fCost //Total cost from astart to end
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
