using System.Collections.Generic;
using _Game.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.LevelMap.Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        private List<Tween> moves = new List<Tween>();
        
        
        public void Move(IntVect delta, float duration)
        {
            var t = transform.DOBlendableLocalMoveBy(delta.ToV3(), duration)
                .SetEase(GameManager.Config.ease);
            AddTween(t);
        }

        private void AddTween(Tweener tween)
        {
            tween.OnComplete(() => RemoveTween(tween));
            moves.Add(tween);
        }

        private void RemoveTween(Tweener t)
        {
            moves.Remove(t);
        }
        
        public void Align(IntVect pos)
        {
            var z = transform.localPosition.z;
            transform.localPosition = pos.ToV3(z);
        }

        public abstract bool ActOn(Tile other, Map map);

        public IntVect LocIntPos => IntVect.FromV3(transform.localPosition);
        public IntVect WorldIntPos => IntVect.FromV3(transform.position);
    }
}