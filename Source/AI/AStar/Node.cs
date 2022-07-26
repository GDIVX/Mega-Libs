using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics.Components.Board.Pathfinding
{
    public class Node
    {
        public WorldTile tile;
        public int gCost, hCost , fCost;

        public Node cameFrom;

        public Node(WorldTile tile){
            try{
                this.tile = tile;
                tile.node = this;
            }
            catch(NullReferenceException){
                Debug.Log($"tile = {tile} node = {this}");
            }
        }

        public override string ToString()
        {
            return tile.ToString();
        }

        internal void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public static Node FindNode(Vector3Int position){
            return WorldController.Instance.GetTile(position).node;
        }

        internal bool CompareTo(Node node)
        {
            return tile.position == node.tile.position;
        }

        internal Node[] GetNeighbors()
        {
            var tiles = tile.GetNeighbors();

            List<Node> res = new List<Node>();

            foreach (var t in tiles)
            {
                res.Add(t.node);
            }

            return res.ToArray();
        }
    }
}