using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class SurfaceGenerator : MonoBehaviour
{
    public Vector2 size = new Vector2(1000, 1000);
    public Transform[] requiredPlateaus;

    public float plateauHeight = 50.0f;
    public float canyonDepth = -50.0f;

    private FastNoise noiseGen;
    private FastNoise detailNoise;
    private Terrain terrain;

    public FastNoise.NoiseType primaryNoiseType = FastNoise.NoiseType.SimplexFractal;
    public FastNoise.NoiseType detailNoiseType = FastNoise.NoiseType.Cellular;
    public int seed = 0;

    public float Frequency = 0.01f;
    public FastNoise.Interp InterpolationType = FastNoise.Interp.Quintic;
    public FastNoise.FractalType FractalType = FastNoise.FractalType.Billow;
    public int Octaves = 3;
    public float Lancunarity = 2.0f;
    public float Gain = 0.5f;

    public float Factor = 1.0f;

    public bool canyons = true;

    public float detailStrength = 0.1f;

    private void Start()
    {
        if (seed == 0) seed = Random.Range(int.MinValue, int.MaxValue);
        noiseGen = new FastNoise();
        noiseGen.SetNoiseType(primaryNoiseType);
        noiseGen.SetFractalGain(Gain);
        noiseGen.SetFractalLacunarity(Lancunarity);
        noiseGen.SetFractalOctaves(Octaves);
        noiseGen.SetFractalType(FractalType);
        noiseGen.SetInterp(InterpolationType);
        noiseGen.SetFrequency(Frequency);

        detailNoise = new FastNoise();
        detailNoise.SetNoiseType(detailNoiseType);

        terrain = GetComponent<Terrain>();

        TerrainData terrainData = terrain.terrainData;
        terrainData.size = new Vector3(size.x, plateauHeight - canyonDepth, size.y);
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        
        for(int y = 0; y < heights.GetLength(1); ++y)
        {
            for (int x = 0; x < heights.GetLength(0); ++x)
            {
                float baseHeight = 1 - noiseGen.GetNoise(x, y).Remap(-1, 1, 0, 1);
                baseHeight = Mathf.Clamp(baseHeight * baseHeight * Factor, detailStrength, 1 - detailStrength);
                float detail = detailNoise.GetNoise(x, y) * detailStrength;
                heights[x, y] = baseHeight + detail;
            }
        }

        terrainData.SetHeights(0, 0, heights);
        terrain.terrainData = terrainData;
    }

}
