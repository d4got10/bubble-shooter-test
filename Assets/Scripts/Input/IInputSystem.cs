using UnityEngine;

namespace BubbleShooter
{
    public interface IInputSystem
    {
        event System.Action Clicked;
        Vector2 MousePosition { get; }
    }
}