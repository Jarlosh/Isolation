using System;
using System.Collections.Generic;

namespace _Game.Scripts.LevelMap.Tiles
{
    public class Unit : Tile
    {
        delegate bool HitHandler<T>(T other) where T : Tile;
        
        public override bool ActOn(Tile other, Map map)
        {
            var d = new Dictionary<Type, HitHandler<Tile>>()
            {
                {typeof(Unit), Value}
            };
        }

        private bool Value(Tile other)
        {
            throw new NotImplementedException();
        }

        private static void Honk<T>() where T: Tile
        {
               
        }
    }
}