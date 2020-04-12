using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.LevelMap;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class InputManager : BaseManager<InputManager>
    {
        private readonly Dictionary<KeyCode, Direction> hotkeys = new Dictionary<KeyCode, Direction>()
        {
            {KeyCode.W, Direction.Up},
            {KeyCode.S, Direction.Down},
            {KeyCode.A, Direction.Left},
            {KeyCode.D, Direction.Right},
        };
        
        private void Update()
        {
            if(Input.anyKey) ProcessInput();
        }

        private void ProcessInput()
        {
            var pressed = hotkeys.Keys.Where(Input.GetKeyDown).ToArray();
            if (pressed.Any())
            {
                var direction = hotkeys[pressed.First()];
                Move(direction);
            }
        }
        
        private void Move(Direction direction)
        {
            LevelManager.Instance.PlanMove(direction);
        }
    }
}
