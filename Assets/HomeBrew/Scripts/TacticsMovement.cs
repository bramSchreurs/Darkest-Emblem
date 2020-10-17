using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMovement : MonoBehaviour
{
    public bool isMyTurn = false;
    List<MapTile> selectableTiles = new List<MapTile>();
    protected GameObject[] tiles;

    Stack<MapTile> path = new Stack<MapTile>();
    MapTile currentTile;

    public int walkingRange;
    public float jumpheight = 2;
    float walkingSpeed = 2;
    public bool moving = false;


    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    // Start is called before the first frame update
    protected void Init(){
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public MapTile GetTargetTile(GameObject target)
    {
        // copy pasta youtube, werkt in 3D, heb het moeten aanpassen voor 2D
        //RaycastHit hit;
        //MapTile tile = null;
        //if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        //{
        //    tile = hit.collider.GetComponent<MapTile>();
        //}

        //eigen versie voor 2D
        MapTile tile = null;
        //op een vieze manier neemt dit gewoon het eerste object van de tiles layer die de ray tegenkomt vertrekkende vanuit target
        RaycastHit2D hit2D = Physics2D.Raycast(target.transform.position, Vector2.right, Mathf.Infinity, LayerMask.GetMask("Tilebois"), -Mathf.Infinity, Mathf.Infinity);

        tile = hit2D.collider.gameObject.GetComponent<MapTile>();
        return tile;
    }
    public void ComputeAdjencencyLists()
    {
        foreach (GameObject tile in tiles)
        {
            MapTile t = tile.GetComponent<MapTile>();
            t.FindEdges(jumpheight);
        }
    }
    public void FindSelectableTiles()
    {
        ComputeAdjencencyLists();
        GetCurrentTile();

        Queue<MapTile> process = new Queue<MapTile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;
        while (process.Count > 0)
        {
            MapTile t = process.Dequeue();
            selectableTiles.Add(t);

            t.selectable = true;

            if (t.distance < walkingRange)
            {

                foreach (MapTile neighbour in t.adjacencyList)
                {
                    if (!neighbour.visited)
                    {
                        neighbour.parent = t;
                        neighbour.visited = true;
                        neighbour.distance = 1 + t.distance;
                        process.Enqueue(neighbour);
                    }
                }
            }
        }
    }
    public void MoveToTile(MapTile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        MapTile next = tile;
        while (next != null)
        {
            next.GetComponent<Renderer>().material.color = Color.blue;
            path.Push(next);
            next = next.parent;
        }
    }
    public void Move()
    {

        if (path.Count > 0)
        {
            MapTile tile = path.Peek();

            Vector3 target = tile.transform.position;
            //voor 3D stuff om uw personage op uw tile te zitten ipv er in.
            //target.y += halfHeight + tile.GetComponent<BoxCollider2D>().bounds.extents.y;
            //if distance between target and current position is really small, you've reached the target
            if (Vector2.Distance(transform.position, target) >= 0.01f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                transform.position.Set(target.x, target.y, transform.position.z);
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            TurnManager.EndTurn();
        }
    }
    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (MapTile mapTile in selectableTiles)
        {
            mapTile.reset();
        }
        selectableTiles.Clear();
    }
    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.z = 0;
        heading.Normalize();

    }
    void SetHorizontalVelocity()
    {
        velocity = heading * walkingSpeed;
    }

    public void BeginTurn(){
        isMyTurn = true;

    }
    public void EndTurn(){
        isMyTurn = false;
    }
}
