﻿using UnityEngine;

 namespace _Game.Scripts.Tools
{
    public static class JuiHelper
    {
        public static Vector3 Divide(Vector3 divided, Vector3 divider) => Vector3.Scale(divided, Reversed(divider));
        public static Vector2 Divide(Vector2 divided, Vector2 divider) => Vector2.Scale(divided, Reversed(divider));
        public static Vector3 Reversed(Vector3 v) => new Vector3(1/v.x, 1/v.y, 1/v.z);
        public static Vector2 Reversed(Vector2 v) => new Vector3(1/v.x, 1/v.y);
    }
}