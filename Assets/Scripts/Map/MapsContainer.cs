using UnityEngine;

namespace BubbleShooter
{
    public class MapsContainer
    {
        private readonly Map[] PrefabMaps;


        private Map _lastMap;


        public MapsContainer()
        {
            PrefabMaps = new Map[]
            {
                CreateFirstMap(),
                CreateSecondMap(),
                CreateThirdMap()
            };
        }


        public Map GetPrefabMap(int index)
        {
            var map = new Map(PrefabMaps[index]);
            _lastMap = PrefabMaps[index];
            return map;
        }

        public Map GetRandomMap()
        {
            var randomMap = CreateRandomMap();
            var map = new Map(randomMap);
            _lastMap = randomMap;
            return map;
        }

        public Map GetLastMap() => new Map(_lastMap);


        private Map CreateRandomMap()
        {
            var sizeX = Game.SizeX;
            var sizeY = Game.SizeY;

            var map = new Map(new Vector2Int(sizeX, sizeY));
            var data = new BubbleType[sizeX, sizeY];
            for (int i = 0; i < sizeX * sizeY; i += 3)
            {
                var type = (BubbleType)(Random.Range(1, 4));
                for (int j = 0; j < 3; j++)
                {
                    int index = i + j;

                    if (index / sizeX < sizeX) continue;
                    if (index % sizeX < 2 || index % sizeX > sizeX - 3) continue;

                    data[index % sizeX, index / sizeX] = type;
                }
            }

            map.Set(data);

            return map;
        }

        private Map CreateFirstMap()
        {
            var sizeX = Game.SizeX;
            var sizeY = Game.SizeY;

            var map = new Map(new Vector2Int(sizeX, sizeY));
            var data = new BubbleType[sizeX, sizeY];
            for (int y = sizeY / 2; y < sizeY; y++)
            {
                for (int x = 1; x < sizeX - 1; x++)
                {
                    data[x, y] = (BubbleType)(y % 3 + 1);
                }
            }

            map.Set(data);

            return map;
        }

        private Map CreateSecondMap()
        {
            var sizeX = Game.SizeX;
            var sizeY = Game.SizeY;

            var map = new Map(new Vector2Int(sizeX, sizeY));
            var data = new BubbleType[sizeX, sizeY];
            for(int y = sizeY - 1; y > sizeY / 2; y--)
            {
                for(int x = sizeY - y; x < sizeX - 1; x++)
                {
                    data[x, y] = (BubbleType)((x + y) % 3 + 1);
                }
            }

            map.Set(data);

            return map;
        }

        private Map CreateThirdMap()
        {
            var sizeX = Game.SizeX;
            var sizeY = Game.SizeY;

            var map = new Map(new Vector2Int(sizeX, sizeY));
            var data = new BubbleType[sizeX, sizeY];
            for (int y = sizeY / 2; y < sizeY; y++)
            {
                data[1, y] = (BubbleType)(y % 3 + 1);
                if (y == sizeY / 2 || y == sizeY - 1)
                {
                    for (int x = 2; x < sizeX - 2; x++)
                        data[x, y] = (BubbleType)(x % 3 + 1);
                }
                data[sizeX - 2, y] = (BubbleType)(y % 3 + 1);
            }

            map.Set(data);

            return map;
        }
    }
}