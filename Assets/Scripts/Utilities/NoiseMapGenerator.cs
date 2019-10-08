using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGenerator
{
    public float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float offsetX, float offsetY, Wave[] waves) {
        // create an empty noise map with the mapDepth and mapWidth coordinates
        float[,] noiseMap = new float[mapWidth*2, mapHeight*2];

        for (int xIndex = 0; xIndex < mapWidth*2; xIndex++) {
            for (int yIndex = 0; yIndex < mapHeight*2; yIndex++) {
                // calculate sample indices based on the coordinates, the scale and the offset
                float sampleX = (xIndex + offsetX) / scale;
                float sampleY = (yIndex + offsetY) / scale;

                if(sampleX == 0) {
                    sampleX = 1/scale;
                }

                if (sampleY == 0) {
                    sampleY = 1/scale;
                }

                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves) {
                    var xValue = sampleX * wave.frequency;
                    var yValue = sampleY * wave.frequency;

                    // generate noise value using PerlinNoise for a given Wave
                    noise += wave.amplitude * Mathf.PerlinNoise(xValue, yValue);
                    normalization += wave.amplitude;
                }
                // normalize the noise value so that it is within 0 and 1
                noise /= normalization;

                noiseMap[xIndex, yIndex] = noise;
            }
        }

        return noiseMap;
    }
}
