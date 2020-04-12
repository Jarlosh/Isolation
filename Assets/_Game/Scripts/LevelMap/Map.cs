using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.LevelMap.Tiles;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

namespace _Game.Scripts.LevelMap
{
    public class Map : MonoBehaviour
    {
        private bool inited = false;
        private HashSet<Tile> tileMap = new HashSet<Tile>();

        public void Init()
        {
            SetTiles(FindTiles());
            inited = true;
            
            AlignTiles();
        }

        private void SetTiles(IEnumerable<Tile> tiles)
        {
            tiles.Foreach(SetTile);
        }

        private IEnumerable<Tile> FindTiles()
        {
            return FindObjectsOfType<Tile>();
        }

        private void SetTile(Tile t)
        {
            tileMap.Add(t);
        }

        public IEnumerable<Tile> Tiles => tileMap.AsEnumerable();
        
        private void AlignTiles()
        {
            tileMap.Foreach(t => t.Align(t.LocIntPos));
        }

        public IEnumerable<T> SelectOfType<T>() where T : Tile
        {
            return tileMap.Where(t => t is T).Cast<T>();
        }

        public bool IsInited => inited;
    }
    public class MapOld : MonoBehaviour
    {
        private bool inited = false;
        private Dictionary<IntVect, HashSet<Tile>> tileMap = new Dictionary<IntVect, HashSet<Tile>>();

        public void Init()
        {
            FindObjectsOfType<Tile>()
                .Foreach(t => SetTile(t, t.LocIntPos));
            inited = true;
            
            AlignTiles();
        }

        private void AlignTiles()
        {
            DoForeach((pos, t) => t.Align(pos));
        }

        private void DoForeach(Action<IntVect, Tile> action)
        {
            foreach (var pos in tileMap.Keys)
            foreach (var tile in tileMap[pos])
                action(pos, tile);
        }

        private IEnumerable<Tile> SelectAll()
        {
            return tileMap.Keys.SelectMany(pos => tileMap[pos]);
        }

        public IEnumerable<T> SelectOfType<T>() where T : Tile
        {
            return SelectAll().Where(t => t is T).Cast<T>();
        }


        public bool IsInited => inited;
        
        private void SetTile(Tile tile, IntVect pos)
        {
            if (!tileMap.ContainsKey(pos))
                tileMap[pos] = new HashSet<Tile>{tile};
            else tileMap[pos].Add(tile);
        }
    }

    public struct IntVect
    {
        public int x;
        public int y;

        public IntVect(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static IntVect FromV3(Vector3 v3)
        {
            return new IntVect(
                Mathf.RoundToInt(v3.x), 
                Mathf.RoundToInt(v3.y));
        }

        
        public static IntVect operator -(IntVect a) => new IntVect(-a.x, -a.y);
        public static IntVect operator +(IntVect a, IntVect b) => new IntVect(a.x + b.x, a.y + b.y);
        public static IntVect operator -(IntVect a, IntVect b) => a + -b;
        public static IntVect operator *(IntVect a, int count) => new IntVect(a.x*count, a.y*count);

        public Vector3 ToV3(float z=0)
        {
            return new Vector3(x, y, z);    
        }

        public float Magnitude()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public static IntVect Up = new IntVect(0, 1);
        public static IntVect Down = new IntVect(0, -1);
        public static IntVect Left = new IntVect(-1, 0);
        public static IntVect Right = new IntVect(1, 0);
    }
}