using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapTile: MonoBehaviour
{
    public int moveCost;
    //public GameObject gameObject;
    public bool passable;
    public string tileName;
    //public Vector3 position;
    public int elevation;
    // stuff from youtube video: https://www.youtube.com/watch?v=cK2wzBCh9cg
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool occupied = false;
    public List<MapTile> adjacencyList = new List<MapTile>();

    //BFS variables
    public bool visited = false;
    public MapTile parent = null;
    public int distance = 0;
    public MapTile(int cost, bool passable, string tileName, int elevation)
    {
        this.moveCost = cost;
        //this.gameObject = gameObject;
        this.passable = passable;
        this.tileName = tileName;
        //this.position = position;
        this.elevation = elevation;
    }
    void Update () {
        if(current) {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if(target) {
            //Debug.Log("target?");
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(selectable) {
            //Debug.Log("selectable?");
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if(occupied) {
            //Debug.Log("selectable?");
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else {
            //Debug.Log("else?");
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void reset(){
        visited = false;
        parent = null;
        distance = 0;

        current = false;
        selectable = false;
        target = false;

        occupied = false;
    }
    void OnMouseDown(){
    }
    public void FindEdges(float jumpHeight){
        reset();
        CheckTile(Vector3.up, jumpHeight);
        CheckTile(-Vector3.up, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);

    }
    public void CheckTile(Vector3 direction, float jumpHeight){
        //Vector3 halfExtends = new Vector3(0.25f, (1+ jumpHeight) / 2.0f, 0.25f);
        //Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + direction, new Vector3(0.5f,0.5f,0.5f),0f, LayerMask.GetMask("Tilebois"), -Mathf.Infinity, Mathf.Infinity);
        /*Debug.Log("number of colliders in checktile =");
        Debug.Log(collider2Ds.Length);
        Debug.Log("currentTile + direction:");
        Debug.Log(transform.position + direction);
        Debug.Log("direction:");
        Debug.Log(direction);
        */
        foreach (var collider in collider2Ds)
        {
            //Debug.Log(collider);
            MapTile tile = collider.GetComponent<MapTile>();
            //Debug.Log("getting tile from collider in checktile");
            //Debug.Log(tile);
            if (tile != null && tile.passable)
            {
                //RaycastHit hit;
                //check of er iets op de tile staat
                /* code van youtube video, werkt voor 3d, heb m'n eigen stuff gedaan voor 2D
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                adjacencyList.Add(tile);
                    
                }
                */
                RaycastHit2D raycast = Physics2D.Raycast(tile.transform.position, Vector3.forward, 0.25F, ~LayerMask.GetMask("Tilebois"), -Mathf.Infinity, Mathf.Infinity);
                if (!raycast)
                {
                    adjacencyList.Add(tile);
                }
                else{
                    //raycast.collider.gameObject.GetComponent<MapTile>().occupied = true;
                }
            }
        }
    }
}

