using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ProceduralTerrain : MonoBehaviour
{
    public int terrainWidth = 256;
    public int terrainHeight = 256;
    public int terrainDepth = 20;
    public float scale = 20f;

    private Terrain terrain;
    private TerrainData terrainData;
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        navMeshSurface = GetComponent<NavMeshSurface>();

        terrainData = new TerrainData();
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, terrainDepth, terrainHeight);
        terrainData.SetHeights(0, 0, GenerateHeights());

        terrain.terrainData = terrainData;

        // Build the NavMesh after generating the terrain
        navMeshSurface.BuildNavMesh();
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[terrainWidth, terrainHeight];
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainHeight; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / terrainWidth * scale;
        float yCoord = (float)y / terrainHeight * scale;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}