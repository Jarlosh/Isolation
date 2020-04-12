using System;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    [Serializable]
    public class GameConfig
    {
        public float MoveDuration = 1f;
        public float MoveCoeff = 1f;

        public int maxMoveQueue = 5;
        public Ease ease = Ease.Linear;
    }
    
    public class GameManager : BaseManager<GameManager>
    {
        [SerializeField] private GameConfig config;

        public static GameConfig Config => Instance.config;
        
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}