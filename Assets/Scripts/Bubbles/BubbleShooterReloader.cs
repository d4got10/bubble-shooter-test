using System.Collections.Generic;
using UnityEngine;

namespace BubbleShooter
{
    public class BubbleShooterReloader : MonoBehaviour
    {
        private IBubbleFactory _bubbleFactory;
        private IReadOnlyMap _map;

        public void Init(IBubbleFactory factory, IReadOnlyMap map)
        {
            _bubbleFactory = factory;
            _map = map;
        }

        public Bubble GetBubble()
        {
            var type = GetRandomRemainingType();

            return _bubbleFactory.Create(type);
        }

        private BubbleType GetRandomRemainingType()
        {
            var remainingTypes = GetRemainingTypes();
            return remainingTypes[Random.Range(0, remainingTypes.Count)];
        }

        private List<BubbleType> GetRemainingTypes()
        {
            var types = new List<BubbleType>();

            for (int x = 0; x < _map.Size.x; x++)
            {
                for (int y = 0; y < _map.Size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    var type = _map[position];

                    if (type == BubbleType.Empty) continue;

                    if (types.Contains(type) == false)
                        types.Add(type);
                }
            }

            return types;
        }
    }
}