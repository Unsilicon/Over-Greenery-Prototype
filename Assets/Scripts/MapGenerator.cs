using UnityEngine;

// 生成游戏内场景地图
public class MapGenerator : MonoBehaviour
{
    public enum Tile
    {
        None,
        Soil
    }

    [SerializeField]
    private int mapSeed;

    [SerializeField]
    private int mapWidth;

    [SerializeField]
    private int mapHeight;

    // 控制噪声比例，越大地形越平缓
    [SerializeField]
    private float mapScale;

    [SerializeField]
    private GameObject tileSoil;

    // 控制输入的地图种子倍率，以免过大的种子号导致地图生成出现问题
    private const float mapSeedScale = 0.01f;

    private Tile[,] terrainMap;

    private void Awake()
    {
        terrainMap = new Tile[mapWidth, mapHeight];
    }

    private void Start()
    {
        GenerateTerrain();
        InstantiateTerrainMap();
    }

    // 生成地形中的大面积tile
    private void GenerateTerrain()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            // 让噪声采样乘以地图高度，以方便后续与Y轴高度相比较
            float sample = Mathf.PerlinNoise(x / mapScale + mapSeed * mapSeedScale, 0f) * mapHeight;
            for (int y = 0; y < mapHeight; y++)
            {
                if (y < sample)
                {
                    terrainMap[x, y] = Tile.Soil;
                }
            }
        }
    }

    // 按照Tile枚举来实例化地形地图
    private void InstantiateTerrainMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject tile = null;
                switch (terrainMap[x, y])
                {
                    case Tile.Soil:
                        tile = tileSoil;
                        break;
                }
                if (tile)
                {
                    Instantiate(tile, new(x - mapWidth / 2f, y - mapHeight / 2f), Quaternion.identity);
                }
            }
        }
    }
}
