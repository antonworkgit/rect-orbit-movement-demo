using System;
using UnityEngine;

namespace Scripts.Input
{
    public interface IInputService
    {
        Vector2 MoveInput { get; }

        event Action JumpInput;
    }
}