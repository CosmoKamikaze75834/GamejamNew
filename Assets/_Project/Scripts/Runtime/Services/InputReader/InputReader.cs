using System;
using UnityEngine;

public class InputReader : IInputReader, IDisposable
{
    private const KeyCode CommandEscape = KeyCode.Escape;
    private const string CommandHorizontalMove = "Horizontal";
    private const string CommandVerticalMove = "Vertical";
    private const int CommandShoot = 0;
    private const int CommandFollowPoint = 1;

    private readonly IUpdateService _updateService;

    public InputReader(IUpdateService updateService)
    {
        _updateService = updateService;

        _updateService.Subscribe(OnUpdate, UpdateType.Update);
    }

    public event Action EscapePressed;
    public event Action FollowPointPressed;
    public event Action ShootPressed;

    public Vector2 Movement { get; private set; }

    public Vector2 PointPosition { get; private set; }

    public void Dispose() =>
        _updateService.Unsubscribe(OnUpdate, UpdateType.Update);

    private void OnUpdate(float deltaTime)
    {
        ReadEscape();
        ReadMovement();
        ReadLook();
        ReadFollowPoint();
        ReadShoot();
    }

    private void ReadEscape()
    {
        if (Input.GetKeyDown(CommandEscape))
            EscapePressed?.Invoke();
    }

    private void ReadMovement()
    {
        Movement = 
            Input.GetAxis(CommandHorizontalMove) * Vector2.right + 
            Input.GetAxis(CommandVerticalMove) * Vector2.up;
    }
    
    private void ReadLook() =>
        PointPosition = Input.mousePosition;

    private void ReadFollowPoint()
    {
        if (Input.GetMouseButton(CommandFollowPoint))
            FollowPointPressed?.Invoke();
    }

    private void ReadShoot()
    {
        if (Input.GetMouseButton(CommandShoot))
            ShootPressed?.Invoke();
    }
}