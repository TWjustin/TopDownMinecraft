using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    public int width = 50;               // 地图宽度
    public int height = 50;              // 地图高度
    public float scale = 10f;            // 噪声网格的缩放值
    public GameObject grassPrefab;       // 草地预制体
    public GameObject waterPrefab;       // 水面预制体
    public float dice = 0.7f;            // 骰子的点数

    public GameObject container;

    private void Awake()
    {
        // 生成地图
        GenerateMap();
    }

    private void GenerateMap()
    {
        float[,] noiseMap = new float[width, height];
        
        // 遍历地图上的每个像素
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 根据噪声网格生成高度值
                float sampleX = x / scale;
                float sampleY = y / scale;
                float noiseValue = Mathf.PerlinNoise(sampleX, sampleY);

                noiseMap[x, y] = noiseValue;
            }
        }
        
        // 根据噪声网格生成地图对象
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 根据噪声值决定地图的属性
                if (noiseMap[x, y] < dice)
                {
                    // 生成地形瓦片、草地、或其他属性
                    Instantiate(grassPrefab, new Vector3(x, y, 0), Quaternion.identity, container.transform);
                }
                else
                {
                    // 生成其他地图元素
                    Instantiate(waterPrefab, new Vector3(x, y, 0), Quaternion.identity, container.transform);
                }
            }
        }
    }
}
