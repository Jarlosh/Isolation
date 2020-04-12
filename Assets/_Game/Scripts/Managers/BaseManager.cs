using System;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
    {
        public static T Instance => GetInstance();
        private static T GetInstance()
        {
            return FindObjectOfType<T>();
        }
        
        public void DoAfter(float duration, Action action) 
            => DOTween.Sequence().SetDelay(duration).OnComplete(() => action());
    }
}