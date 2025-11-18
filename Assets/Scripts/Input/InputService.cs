using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Input
{
    // Handle both PC + mobile
    public sealed class InputService : IInputService, IDisposable
    {
        public Vector2 MoveInput { get; private set; } = Vector2.zero;
        public event Action JumpInput;

        private readonly InputActions _actions;

        public InputService()
        {
            _actions = new InputActions();
            _actions.Player.Enable();

            _actions.Player.Move.started += OnMoveInput;
            _actions.Player.Move.performed += OnMoveInput;
            _actions.Player.Move.canceled += OnMoveCanceled;

            _actions.Player.Jump.performed += OnJumpInput;
        }

        public void Dispose()
        {
            _actions.Player.Move.started -= OnMoveInput;
            _actions.Player.Move.performed -= OnMoveInput;
            _actions.Player.Move.canceled -= OnMoveCanceled;

            _actions.Player.Jump.performed -= OnJumpInput;

            _actions.Dispose();
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveInput = Vector2.zero;
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            JumpInput?.Invoke();
        }
    }
}