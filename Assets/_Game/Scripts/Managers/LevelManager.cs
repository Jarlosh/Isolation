using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.LevelMap;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    [Serializable]
    public class MoveQueue
    {
        private Queue<Direction> planned = new Queue<Direction>();
        private int inProgress;
        private Direction currentDir = Direction.None;
        
        private bool IsMoving => currentDir != Direction.None;

        
        public void Enqueue(Direction direction)
        {
            planned.Enqueue(direction);
        }

        public void OnDone()
        {
            
        }

        private void MakeMoves()
        {
            if (planned.Any())
            {
                var direction = Peek();
                if(!IsMoving || direction == currentDir)
                {
                    var count = GetRepeatedNextCount(direction);
                    RemoveFirst(count);
                    inProgress += count;
                    currentDir = direction;

                    var duration = LevelMan.GetMoveDuration(count);
                    LevelMan.Move(direction, count, duration);
                    LevelMan.DoAfter(duration, () => OnMoved(count));
                }
            }
        
        }

        private LevelManager LevelMan => LevelManager.Instance;

        private void OnMoved(int finishedCount)
        {
            inProgress -= finishedCount;
            if (inProgress == 0)
            {
                currentDir = Direction.None;
            }
            MakeMoves();
        }

        public void PlanMove(Direction direction)
        {
            if(planned.Count <= GameManager.Config.maxMoveQueue)
                Enqueue(direction);
            MakeMoves();
        }
        
        

        private void RemoveFirst(int count)
        {
            for (var i = 0; i < count; i++)
                planned.Dequeue();
        }

        private Direction Peek() => planned.Peek();
        private int GetRepeatedNextCount(Direction direction)
        {
            return planned
                .TakeWhile(d => d == direction)
                .Count();
        }
    }
    
    public class LevelManager : BaseManager<LevelManager>
    {
        [SerializeField] private Level currentLevel;
        [SerializeField] private MoveQueue moveQueue;
        
        public void Move(Direction direction, int distance, float duration)
        {
            currentLevel.Move(direction, duration, distance);
        }

        public void PlanMove(Direction direction)
        {
            moveQueue.PlanMove(direction);
        }


        public float GetMoveDuration(int distance)
        {
            var coeff = Mathf.Pow(GameManager.Config.MoveCoeff, distance);
            return coeff * distance * GameManager.Config.MoveDuration;
        }

    }
}
