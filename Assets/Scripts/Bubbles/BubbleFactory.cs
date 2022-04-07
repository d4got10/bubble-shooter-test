using System.Collections.Generic;
using UnityEngine;

namespace BubbleShooter
{
    public class BubbleFactory : MonoBehaviour, IBubbleFactory
    {
        [SerializeField] private Bubble _prefab;


        public Bubble Create(BubbleType type)
        {
            var bubble = Instantiate(_prefab);

            bubble.Init(type);

            bubble.Painter.SetColor(BubbleUtils.GetColor(type));

            return bubble;
        }
    }
}