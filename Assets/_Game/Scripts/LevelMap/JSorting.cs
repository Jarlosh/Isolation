using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.LevelMap.Tiles;
using UnityEngine;

namespace _Game.Scripts.LevelMap
{
    public class JSorting
    {
        private static Dictionary<Direction, Func<IntVect, int>> sortSelectors = 
            new Dictionary<Direction, Func<IntVect, int>>
            {
            {Direction.Up, SelectorUp},
            {Direction.Down, SelectorDown},
            {Direction.Left, SelectorLeft},
            {Direction.Right, SelectorRight},            
        };
        
        private static int SelectorDown(IntVect pos) => pos.y;
        private static int SelectorLeft(IntVect pos) => pos.x;
        private static int SelectorUp(IntVect pos) => -SelectorDown(pos);
        private static int SelectorRight(IntVect pos) => -SelectorLeft(pos);

        public Tile[] SortTiles(IEnumerable<Tile> tiles, Direction direction)
        {
            var selector = sortSelectors[direction];
            return tiles.OrderBy(t => selector(t.LocIntPos)).ToArray();
        }
        
        
    }
}