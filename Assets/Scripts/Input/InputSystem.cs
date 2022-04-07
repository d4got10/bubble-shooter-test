using System;
using UnityEngine;

namespace BubbleShooter
{
    public class InputSystem : IInputSystem
    {
        public event Action Clicked;


        private readonly InputImage InputImage;


        public Vector2 MousePosition { get; private set; }


        public InputSystem(InputImage inputImage)
        {
            InputImage = inputImage;
        }


        public void Init()
        {
            InputImage.PointerDown += OnPointerDown;
            InputImage.PointerUp += OnPointerUp;
        }

        public void Deinit()
        {
            InputImage.PointerDown -= OnPointerDown;
        }


        private void OnPointerUp()
        {
            Clicked?.Invoke();
        }
        private void OnPointerDown(Vector2 position)
        {
            MousePosition = position;
        }
    }
}