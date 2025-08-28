using UnityEngine;

// ������Ϸ�ڳ�����ͼ
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

    // ��������������Խ�����Խƽ��
    [SerializeField]
    private float mapScale;

    [SerializeField]
    private GameObject tileSoil;

    // ��������ĵ�ͼ���ӱ��ʣ������������Ӻŵ��µ�ͼ���ɳ�������
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

    // ���ɵ����еĴ����tile
    private void GenerateTerrain()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            // �������������Ե�ͼ�߶ȣ��Է��������Y��߶���Ƚ�
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

    // ����Tileö����ʵ�������ε�ͼ
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
