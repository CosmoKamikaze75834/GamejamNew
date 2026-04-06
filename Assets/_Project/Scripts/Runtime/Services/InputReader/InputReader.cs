using System;
using UnityEngine;

public class InputReader : IInputReader, IDisposable
{
    private const KeyCode KeyEscape = KeyCode.Escape;
    private const KeyCode KeyShoot = KeyCode.Space;
    private const KeyCode KeyRotateLeft = KeyCode.Q;
    private const KeyCode KeyRotateRight = KeyCode.E;
    private const KeyCode KeyRotateLeft2 = KeyCode.LeftArrow;
    private const KeyCode KeyRotateRight2 = KeyCode.RightArrow;
    private const string AxisHorizontalMove = "Horizontal";
    private const string AxisVerticalMove = "Vertical";
    private const int MouseShoot = 0;
    private const int CommandFollowPoint = 1;

    private readonly IUpdateService _updateService;
    private readonly float _rotationAcceleration = 2f;
    private readonly float _rotationDeceleration = 30f;
    private float _targetRotationAim;

    public InputReader(IUpdateService updateService)
    {
        _updateService = updateService;

        _updateService.Subscribe(OnUpdate, UpdateType.Update);
    }

    public event Action EscapePressed;
    public event Action FollowPointPressed;
    public event Action ShootPressed;

    public Vector2 Movement { get; private set; }

    public Vector2? PointPosition { get; private set; }

    public float RotationAim { get; private set; }

    public void Dispose() =>
            _updateService.Unsubscribe(OnUpdate, UpdateType.Update);

    private void OnUpdate(float deltaTime)
    {
        ReadEscape();
        ReadMovement();
        ReadLook();
        ReadFollowPoint();
        ReadShoot();

        if (_targetRotationAim != RotationAim)
        {
            float step = (_targetRotationAim == 0)
                ? _rotationDeceleration * deltaTime
                : _rotationAcceleration * deltaTime;
            RotationAim = Mathf.MoveTowards(RotationAim, _targetRotationAim, step);
        }
    }

    private void ReadEscape()
    {
        if (Input.GetKeyDown(KeyEscape))
            EscapePressed?.Invoke();
    }

    private void ReadMovement()
    {
        Movement =
            Input.GetAxis(AxisHorizontalMove) * Vector2.right +
            Input.GetAxis(AxisVerticalMove) * Vector2.up;
    }

    private void ReadLook()
    {
        if (Input.mousePositionDelta != Vector3.zero)
        {
            PointPosition = Input.mousePosition;
            _targetRotationAim = 0;
        }
        else if (Input.GetKey(KeyRotateLeft) || Input.GetKey(KeyRotateLeft2))
        {
            PointPosition = null;
            _targetRotationAim = -1;
        }
        else if (Input.GetKey(KeyRotateRight) || Input.GetKey(KeyRotateRight2))
        {
            PointPosition = null;
            _targetRotationAim = 1;
        }
        else
        {
            _targetRotationAim = 0;
        }
    }

    private void ReadFollowPoint()
    {
        if (Input.GetMouseButton(CommandFollowPoint))
            FollowPointPressed?.Invoke();
    }

    private void ReadShoot()
    {
        if (Input.GetMouseButton(MouseShoot) || Input.GetKey(KeyShoot))
            ShootPressed?.Invoke();
    }
}