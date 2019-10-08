using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public int MapWidthTiles;
    public int MapHeightTiles;

    public ObstacleType[] ObstacleTypes;

    public float MapScale;

    public Wave[] Waves;

    NoiseMapGenerator _noiseMapGeneration;

    float _tileWidth;
    float _tileHeight;

    float[,] _heightMap;

    int _spawnCount = 0;

    void Start() {
        _noiseMapGeneration = new NoiseMapGenerator();
        GenerateHeightMap();

        GenerateTiles();
    }

    void GenerateHeightMap() {
        var offsetX = -gameObject.transform.position.x;
        var offsetZ = -gameObject.transform.position.z;

        _heightMap = _noiseMapGeneration.GenerateNoiseMap(MapWidthTiles, MapHeightTiles, MapScale, offsetX, offsetZ, Waves);
    }

    void GenerateTiles() {
        // for each Tile, instantiate a Tile in the correct position
        for (int xTileIndex = 0; xTileIndex < MapWidthTiles; xTileIndex++) {
            for (int yTileIndex = 0; yTileIndex < MapHeightTiles; yTileIndex++) {
                // calculate the tile position based on the X and Z indices
                var tilePosition = new Vector3(xTileIndex, yTileIndex,-1);

                var obstacle = ChooseObstacle(_heightMap[xTileIndex, yTileIndex]);
                if(obstacle != null) {
                    // instantiate a new Tile
                    GameObject obstacleObj = Instantiate(obstacle, tilePosition, transform.rotation) as GameObject;
                    obstacleObj.transform.parent = gameObject.transform;

                }
            }
        }
    }

    GameObject ChooseObstacle(float height) {
        // for each terrain type, check if the height is lower than the one for the terrain type
        foreach (var obstacle in ObstacleTypes) {
            // return the first terrain type whose height is higher than the generated one
            if (height > obstacle.MinHeight && height < obstacle.MaxHeight) {
                if(obstacle.Name == "spawns") {
                    if(_spawnCount <= 5) {
                        _spawnCount++;

                        return obstacle.GetPrefab(height); ;
                    }

                    return null;
                }

                return obstacle.GetPrefab(height); ;
            }
        }

        return null;
    }
}