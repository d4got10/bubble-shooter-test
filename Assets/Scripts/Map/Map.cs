using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleShooter
{
    public delegate void OnMapChanged(Vector2Int position);

    public class Map : IReadOnlyMap
    {
        public event OnMapChanged Changed;


        public Vector2Int Size { get; }
        public BubbleType this[Vector2Int position] => _data[position.x, position.y];


        private readonly BubbleType[,] _data;


        public Map(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(size), "Map dimensions must be positive.");

            _data = new BubbleType[size.x, size.y];
            Size = size;
        }

        public Map(Map copy)
        {
            Size = copy.Size;
            _data = new BubbleType[Size.x, Size.y];
            for (int x = 0; x < Size.x; x++)
                for (int y = 0; y < Size.y; y++)
                    _data[x, y] = copy._data[x, y];
        }


        public void Set(BubbleType[,] data)
        {
            var dataSize = new Vector2Int(data.GetLength(0), data.GetLength(1));

            if (dataSize.x > Size.x || dataSize.y > Size.y)
                throw new System.ArgumentOutOfRangeException(nameof(data), "Data's size must not be greater then map size");

            for(int x = 0; x < dataSize.x; x++)
            {
                for(int y = 0; y < dataSize.y; y++)
                {
                    _data[x, y] = data[x, y];
                    Changed?.Invoke(new Vector2Int(x, y));
                }
            }
        }

        public void Set(ICollection<Vector2Int> positions, BubbleType type)
        {
            foreach(var position in positions)
            {
                _data[position.x, position.y] = type;
                Changed?.Invoke(position);
            }
        }

        public void Set(Vector2Int position, BubbleType type)
        {
            _data[position.x, position.y] = type;
            Changed?.Invoke(position);
        }
    }
}