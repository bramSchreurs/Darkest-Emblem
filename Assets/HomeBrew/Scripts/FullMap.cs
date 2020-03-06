using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FullMap : MonoBehaviour
{
    public MapTile[] mapTileArray;
    int[,] tiles;
    int mapSizeX = 10;
    int mapSizeY = 10;

    private void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];
        //grass = 0
        for(int x=0; x< mapSizeX; x++)
        {
            for(int y=0; y< mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        //mountains = 1
        tiles[0, 0] = 1;
        tiles[0, 1] = 1;
        tiles[1, 0] = 1;
        tiles[9, 0] = 1;
        tiles[9, 1] = 1;
        tiles[8, 0] = 1;
        tiles[0, 9] = 1;
        tiles[1, 9] = 1;
        tiles[0, 8] = 1;
        tiles[9, 9] = 1;
        tiles[9, 8] = 1;
        tiles[8, 9] = 1;

        //dessert = 2
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 3; y < 5; y++)
            {
                tiles[x, y] = 2;
            }
        }
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 7; y < 8; y++)
            {
                tiles[x, y] = 2;
            }
        }
        //water = 3
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 5; y < 7; y++)
            {
                tiles[x, y] = 3;
            }
        }
        generateMapVisuals();
        
    }
    void generateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                MapTile tile = mapTileArray[tiles[x, y]];
                Instantiate(tile.gameObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
