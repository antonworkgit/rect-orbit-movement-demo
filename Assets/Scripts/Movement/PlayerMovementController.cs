using Scripts.Input;
using Scripts.Path;
using Scripts.Settings;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Scripts.Movement
{
    // Simple demo, no FSM
    public sealed class PlayerMovementController : MonoBehaviour
    {
        private float _moveSpeed;
        private float _baseOffset;
        private AnimationCurve _jumpCurve;
        private float _jumpHeight;
        private float _jumpDuration;

        // Real distance
        private float _position = 0f;
        private float _jumpTime = 0f;
        private bool _isJumping = false;

        private IInputService _inputService;
        private IOrbPath _path;

        [Inject]
        private void Construct(IInputService inputService, IOrbPath path, PlayerMovementSettings movementSettings)
        {
            _inputService = inputService;
            _path = path;

            _moveSpeed = movementSettings.MoveSpeed;
            _baseOffset = movementSettings.BaseOffset;
            _jumpCurve = movementSettings.JumpCurve;
            _jumpHeight = movementSettings.JumpHeight;
            _jumpDuration = movementSettings.JumpDuration;
        }

        public void SetInitPath(IOrbPath path)
        {
            _path = path;
            _position = 0f;
        }

        private void OnEnable()
        {
            _inputService.JumpInput += OnJump;
        }


        private void OnDisable()
        {
            _inputService.JumpInput -= OnJump;
        }
        private void OnJump()
        {
            if (!_isJumping)
            {
                _jumpTime = 0f;
                _isJumping = true;
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleJump();
            SetPositionOnSpline();
        }

        private void HandleMovement()
        {
            float input = _inputService.MoveInput.x;
            _position += input * _moveSpeed * Time.deltaTime;
            _position = Mathf.Repeat(_position, _path.Perimeter);
        }

        private void HandleJump()
        {
            if (_isJumping)
            {
                _jumpTime += Time.deltaTime;

                if (_jumpTime >= _jumpDuration)
                {
                    _jumpTime = _jumpDuration;
                    _isJumping = false;
                }
            }
        }

        private float GetJumpOffset()
        {
            if (!_isJumping) return 0f;

            float jumpT = _jumpTime / _jumpDuration;
            return _jumpCurve.Evaluate(jumpT) * _jumpHeight;
        }

        private void SetPositionOnSpline()
        {
            _path.EvalP(_position, out var pos, out var tangent, out _);

            // Z rotation for 2d
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;

            // Perpendicular offset
            float3 normal = new Vector3(-tangent.y, tangent.x).normalized;
            float3 offset = normal * (_baseOffset + GetJumpOffset());
            Vector3 finalPosition = pos + offset;

            transform.SetPositionAndRotation(finalPosition, Quaternion.Euler(0f, 0f, angle));
        }
    }
}