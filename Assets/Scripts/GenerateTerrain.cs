using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public int MapWidthTiles;
    public int MapHeightTiles;

    public GameObject TilePrefab;

    public float MapScale;

    public TerrainType[] TerrainTypes;

    public Wave[] Waves;

    NoiseMapGenerator _noiseMapGeneration;

    float _tileWidth;
    float _tileHeight;

    float[,] _heightMap;

    void Start() {
        _noiseMapGeneration = new NoiseMapGenerator();
        // get the tile dimensions from the tile Prefab
        Vector3 tileSize = TilePrefab.GetComponent<MeshRenderer>().bounds.size;
        _tileWidth = tileSize.x;
        _tileHeight = tileSize.z;

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
                var xIndex = transform.position.x + xTileIndex * _tileWidth;
                var yIndex = transform.position.y + yTileIndex * _tileHeight;

                // calculate the tile position based on the X and Z indices
                var tilePosition = new Vector3(xIndex, yIndex,0);

                // instantiate a new Tile
                GameObject tile = Instantiate(TilePrefab, tilePosition, transform.rotation) as GameObject;

                var newTile = tile.GetComponent<MeshRenderer>();
                var tileTexture = BuildTexture(xTileIndex, yTileIndex); //, _tileWidth, _tileHeight);
                newTile.material.mainTexture = tileTexture;
                newTile.material.shader = Shader.Find("Sprites/Default");
                tile.transform.parent = gameObject.transform;
            }
        }
    }

    private Texture2D BuildTexture(int x, int y) { 

        var mapValue = _heightMap[Mathf.Abs(x), Mathf.Abs(y)];
        TerrainType terrainType = ChooseTerrainType(mapValue);

        return terrainType.SpriteTexture;
    }

    TerrainType ChooseTerrainType(float height) {
        // for each terrain type, check if the height is lower than the one for the terrain type
        foreach (TerrainType terrainType in TerrainTypes) {
            // return the first terrain type whose height is higher than the generated one
            if (height < terrainType.height) {
                return terrainType;
            }
        }
        return TerrainTypes[TerrainTypes.Length - 1];
    }
}