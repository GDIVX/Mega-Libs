using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Mechanics.Components.Board.Pathfinding
{
    public class Pathfinder
    {
        List<Node> openList , ClosedList;

        public List<Node> FindPath(WorldTile start , WorldTile end , bool flying = false){

            Node startNode = new Node(start);

            openList = new List<Node>{startNode};
            ClosedList = new List<Node>();

            foreach (var tile in WorldController.Instance.world)
            {
                Node node = new Node(tile);
                node.gCost = tile.speedCost;
                node.CalculateFCost();
                node.cameFrom = null;
            }

            startNode.gCost = 0;
            startNode.hCost = WorldController.DistanceOf(start.position , end.position);

            while(openList.Count > 0){
                Node currentNode = GetLowestFCostNode(openList);
                if(currentNode.tile == end){
                    // Reached final node
                    return CalculatePath(currentNode);
                }

                openList.Remove(currentNode);
                ClosedList.Add(currentNode);

                foreach (var neighbor in currentNode.GetNeighbors())
                {
                    if(ClosedList.Contains(neighbor)) continue;

                    if(!neighbor.tile.walkable && !flying || neighbor.tile.CreatureID != 0){
                        ClosedList.Add(neighbor);
                        continue;
                    }
                    if(currentNode.gCost < neighbor.gCost){
                        neighbor.cameFrom = currentNode;
                        neighbor.gCost = currentNode.gCost;
                        neighbor.hCost = WorldController.DistanceOf(neighbor.tile.position , end.position);
                        neighbor.CalculateFCost();
                    }

                    if(!openList.Contains(neighbor)){
                        openList.Add(neighbor);
                    }
                }
            }

            //out of nodes on the open list
            //no path is possible 
            return null;
        }

        private List<Node> CalculatePath(Node endNode)
        {
            List<Node> path = new List<Node>();
            path.Add(endNode);
            Node currentNode = endNode;

            while(currentNode.cameFrom != null){
                path.Add(currentNode.cameFrom);
                currentNode = currentNode.cameFrom;
            }

            path.Reverse();

            return path;
        }

        Node GetLowestFCostNode(List<Node> nodes){
            Node res = nodes[0];
            foreach (var node in nodes)
            {
                if(node.fCost < res.fCost)
                    res = node;
            }

            return res;
        }
    }
}