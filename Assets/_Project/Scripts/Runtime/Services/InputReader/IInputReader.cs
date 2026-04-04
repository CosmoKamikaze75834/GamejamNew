using System;
using UnityEngine;

public interface IInputReader 
{
    public event Action EscapePressed;

    public event Action FollowPointPressed;

    public Vector2 Movement {  get; }

    public Vector2 PointPosition { get; }
}