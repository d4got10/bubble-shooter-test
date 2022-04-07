using System;
using UnityEngine;

namespace BubbleShooter
{
    public static class BubbleUtils
    {
        public static Color GetColor(BubbleType type)
        {
            return type switch
            {
                BubbleType.Empty => Color.black,
                BubbleType.Red => Color.red,
                BubbleType.Green => Color.green,
                BubbleType.Blue => Color.blue,
                _ => throw new NotImplementedException(),
            };
        }
    }
}