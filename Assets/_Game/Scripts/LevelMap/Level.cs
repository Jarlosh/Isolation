using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.LevelMap.Tiles;
using _Game.Scripts.Managers;
using _Game.Scripts.Tools;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Scripts.LevelMap
{
    [Serializable]
    public class LevelStatistic
    {
        [Required, SerializeField] private Level level;
 
        private int HousesCount => level.Houses.Count();
        private int UnitsCount => level.Units.Count();
        private int WallCount => level.Walls.Count();
    }

    public enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }
    
    public class Level : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private LevelStatistic stat;

        private Dictionary<Direction, IntVect> offsets = new Dictionary<Direction, IntVect>
        {
            {Direction.Up, IntVect.Up},
            {Direction.Down, IntVect.Down},
            {Direction.Left, IntVect.Left},
            {Direction.Right, IntVect.Right}
        };
        
        private void Start()
        {
            if(!map.IsInited) map.Init();
            
            
        }

        public IEnumerable<House> Houses => map.SelectOfType<House>();
        public IEnumerable<Wall> Walls => map.SelectOfType<Wall>();
        public IEnumerable<Unit> Units => map.SelectOfType<Unit>();

        public void Move(Direction direction, float duration, int distance)
        {
            var offset = offsets[direction] * distance;
            foreach (var unit in Units)
            {
                var map = MakeMap(this.map.Tiles);
                // unit.Move(offset, duration);
            }
        }

        
        
        
        private Dictionary<IntVect, HashSet<Tile>> MakeMap(IEnumerable<Tile> mapTiles)
        {
            var tileMap = new Dictionary<IntVect, HashSet<Tile>>();
            foreach (var tile in mapTiles)
            {
                var pos = tile.LocIntPos;
                if (!tileMap.ContainsKey(pos))
                    tileMap[pos] = new HashSet<Tile>{tile};
                else tileMap[pos].Add(tile);
            }
            return tileMap;
        }

        public void Move2(Direction direction, Dictionary<IntVect, HashSet<Tile>> mapping)
        {
            foreach (var unit in Units)
            {
                var pos = unit.LocIntPos;
                var hit = mapping[pos].FirstOrDefault(); //todo: foreach
                if (unit.ActOn(hit, map))
                {
                    
                }

            }
            
            
        }
    }
}