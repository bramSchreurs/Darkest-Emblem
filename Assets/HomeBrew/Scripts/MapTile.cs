using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapTile
{
    public int moveCost;
    public GameObject gameObject;
    public bool passable;
    public string tileName;
    //public Vector3 position;
    public int elevation;

    public MapTile(int cost, GameObject gameObject, bool passable, string tileName, Vector3 position, int elevation)
    {
        this.moveCost = cost;
        this.gameObject = gameObject;
        this.passable = passable;
        this.tileName = tileName;
        //this.position = position;
        this.elevation = elevation;
    }
}

