using System;
using UnityEngine;

namespace Scripts.Settings
{
    [Serializable]
    public sealed class PlayerMovementSettings
    {
        public float BaseOffset;
        public float MoveSpeed;
        public AnimationCurve JumpCurve;
        public float JumpHeight;
        public float JumpDuration;
    }
}