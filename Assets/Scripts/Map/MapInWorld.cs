using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BubbleShooter
{
    public class MapInWorld : MonoBehaviour, IBubbleToMapSnapper
    {
        private const int FloorLevel = 5;


        public event System.Action ClearedAll;
        public event System.Action ReachedFloor;


        public IReadOnlyMap Model => _model;

        private Map _model;
        private IBubbleFactory _bubbleFactory;


        private Dictionary<Vector2Int, Bubble> _data;
        private List<Vector2Int> _oddNeighboursDirections = new List<Vector2Int>()
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
        };
        private List<Vector2Int> _evenNeighboursDirections = new List<Vector2Int>()
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
        };


        public void Init(Map model, IBubbleFactory bubbleFactory)
        {
            _data = new Dictionary<Vector2Int, Bubble>();

            _model = model;
            _bubbleFactory = bubbleFactory;

            PopulateDataFromModel(_model);

            _model.Changed += OnModelChanged;
        }

        public void Deinit()
        {
            _model.Changed -= OnModelChanged;

            ClearMap();
        }

        public void SnapToMap(Bubble bubble, Vector2 position)
        {
            var mapPosition = position - (Vector2)transform.position;
            var relativePosition = MapUtils.WorldToRelativePosition(mapPosition);
            _model.Set(relativePosition, bubble.Type);

            var formedCluster = GetCluster(relativePosition, true);
            if (formedCluster.Count > 2)
            {
                _model.Set(formedCluster, BubbleType.Empty);
            }

            var clusters = GetAllClusters();
            foreach(var cluster in clusters)
            {
                if (CheckClusterIsFloating(cluster))
                    _model.Set(cluster, BubbleType.Empty);
            }

            if (CheckMapIsEmpty())
            {
                ClearedAll?.Invoke();
                return;
            }

            if(relativePosition.y < FloorLevel)
            {
                ReachedFloor?.Invoke();
                return;
            }
        }


        private void PopulateDataFromModel(Map model)
        {
            for (int x = 0; x < model.Size.x; x++)
            {
                for (int y = 0; y < model.Size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    SetBubbleTypeAt(position, model[position]);
                }
            }
        }

        private void OnModelChanged(Vector2Int position)
        {
            SetBubbleTypeAt(position, _model[position]);
        }

        private void SetBubbleTypeAt(Vector2Int position, BubbleType bubbleType)
        {
            if (bubbleType == BubbleType.Empty)
                DestroyBubbleAt(position);
            else
                CreateBubbleAt(position, bubbleType);
        }

        private void DestroyBubbleAt(Vector2Int position)
        {
            if (_data.TryGetValue(position, out var bubble))
            {
                Destroy(bubble.gameObject);
                _data.Remove(position);
            }
        }

        private void CreateBubbleAt(Vector2Int position, BubbleType cellType)
        {
            var bubble = _bubbleFactory.Create(cellType);

            bubble.transform.parent = transform;

            var gridPosition = MapUtils.RelativeToWorldPosition(position);
            bubble.transform.localPosition = gridPosition;

            _data[position] = bubble;
        }

        private List<Vector2Int> GetCluster(Vector2Int startPosition, bool typeMatch)
        {
            var cluster = new List<Vector2Int>();
            var visited = new HashSet<Vector2Int>();

            var neighbours = new Stack<Vector2Int>();

            var targetType = _model[startPosition];

            neighbours.Push(startPosition);

            while(neighbours.Count > 0)
            {
                var position = neighbours.Pop();

                if (PositionInOutOfRange(position)) continue;
                if (_model[position] == BubbleType.Empty) continue;
                if (typeMatch && _model[position] != targetType) continue;

                cluster.Add(position);
                visited.Add(position);

                AddNeighboursToStackExceptVisited(neighbours, position, visited);
            }

            return cluster;
        }

        private void AddNeighboursToStackExceptVisited(Stack<Vector2Int> stack, Vector2Int position, HashSet<Vector2Int> visited)
        {
            var directions = position.y % 2 != 0 ? _evenNeighboursDirections : _oddNeighboursDirections;
            foreach(var direction in directions)
            {
                var neighbour = position + direction;
                if (visited.Contains(neighbour)) continue;

                stack.Push(neighbour);
            }
        }

        private bool PositionInOutOfRange(Vector2Int position)
        {
            return position.x < 0 || position.x >= _model.Size.x || position.y < 0 || position.y >= _model.Size.y;
        }

        private List<List<Vector2Int>> GetAllClusters()
        {
            var clusters = new List<List<Vector2Int>>();
            var visited = new HashSet<Vector2Int>();

            for(int x = 0; x < _model.Size.x; x++)
            {
                for(int y = 0; y < _model.Size.y; y++)
                {
                    var position = new Vector2Int(x, y);

                    if (visited.Contains(position)) continue;
                    visited.Add(position);

                    var cluster = GetCluster(position, false);
                    if (cluster.Count == 0) continue;

                    foreach (var positionInCluster in cluster)
                        visited.Add(positionInCluster);

                    clusters.Add(cluster);
                }
            }

            return clusters;
        }

        private bool CheckClusterIsFloating(ICollection<Vector2Int> cluster)
        {
            foreach(var position in cluster)
            {
                if (position.y == _model.Size.y - 1)
                    return false;
            }

            return true;
        }

        private bool CheckMapIsEmpty()
        {
            for(int x = 0; x < _model.Size.x; x++)
            {
                for(int y = 0; y < _model.Size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    if (_model[position] != BubbleType.Empty)
                        return false;
                }
            }

            return true;
        }

        private void ClearMap()
        {
            foreach(var bubble in _data.Values)
            {
                Destroy(bubble.gameObject);
            }
            _data.Clear();
        }
    }
}